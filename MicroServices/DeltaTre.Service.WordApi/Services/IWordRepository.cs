namespace DeltaTre.Service.WordApi.Services
{
	public interface IWordRepository
	{
		IQueryable<Word> Get();

		Word Get(string term);

		void Update(string[] searchTerms);

		void UpdateSearchCount(Word word);
	}
}

