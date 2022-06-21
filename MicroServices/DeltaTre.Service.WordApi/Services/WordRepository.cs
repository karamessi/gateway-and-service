using DeltaTre.Service.WordApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DeltaTre.Service.WordApi.Services
{
	public class WordRepository : IWordRepository
	{
        private readonly WordContext _context;

		public WordRepository(WordContext context)
		{
            _context = context;
		}

        public IQueryable<Word> Get() => _context.Words;

        public Word Get(string term) => _context.Words.FirstOrDefault(word => word.Term.Equals(term, StringComparison.OrdinalIgnoreCase));

        public void Update(string[] searchTerms)
        {
            var lowerCaseTerms = searchTerms.Select(term => term.ToLower());

            var wordsToRemove = _context
                .Words
                .Where(word => !lowerCaseTerms.Contains(word.Term.ToLower()))
                .AsEnumerable();

            var newTerms = searchTerms.Where(term => !_context.Words.Any(word => term.ToLower().Equals(word.Term, StringComparison.OrdinalIgnoreCase)));

            _context.Words.RemoveRange(wordsToRemove);
            _context.Words.AddRange(newTerms.Select(term => new Word(term)));
            _context.SaveChanges();
        }

        public void UpdateSearchCount(Word word)
        {
            // added a bit of extra logic here for concurrency issues.
            // maybe a queue with FIFO but if it fails there more headaches. :)
            var saveFailed = false;
            do
            {
                try
                {
                    word.SearchCount++;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update original values from the database
                    var entry = ex.Entries.Single();
                    var databaseValues = entry?.GetDatabaseValues();
                    if (databaseValues == null)
                    {
                        throw new Exception("Error updating search count");
                    }

                    entry?.OriginalValues.SetValues(databaseValues);
                }

            } while (saveFailed);
        }
    }
}

