namespace DeltaTre.Service.WordApi
{
	public class Word
	{
		public long Id { get; set; }
		public string Term { get; private set; }
		public long SearchCount { get; set; }

		public Word(string term)
        {
			SetTerm(term);
			SearchCount = 0;
        }

		public void SetTerm(string term)
        {
			if (string.IsNullOrWhiteSpace(term))
            {
				throw new Exception("No word provided");
            }

			Term = term;
        }
	}
}

