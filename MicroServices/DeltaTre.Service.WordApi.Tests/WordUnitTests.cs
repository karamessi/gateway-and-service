using System;
namespace DeltaTre.Service.WordApi.Tests
{
	public class WordUnitTests
	{
		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Word_ThrowsException_WhenNullOrWhiteSpace(string testValue)
        {
			Assert.Throws<Exception>(() => new Word(testValue));
        }

		[Fact]
		public void Word_ValidValue()
        {
			var word = new Word("Awesome");
			Assert.NotNull(word);
			Assert.Equal("Awesome", word.Term);
			Assert.Equal(0, word.SearchCount);
        }

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Word_Update_ThrowsException_WhenNullOrWhiteSpace(string updateValue)
        {
			var word = new Word("Awesome");
			Assert.NotNull(word);
			Assert.Equal("Awesome", word.Term);

			Assert.Throws<Exception>(() => word.SetTerm(updateValue));
		}
	}
}

