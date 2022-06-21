using DeltaTre.Service.WordApi.Data;
using DeltaTre.Service.WordApi.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DeltaTre.Service.WordApi.Tests;

public class WordRepositoryUnitTests
{
    [Fact]
    public void Get_Returns_NoEntries()
    {
        var data = new List<Word>().AsQueryable();

        var mockSet = new Mock<DbSet<Word>>();
        mockSet.As<IQueryable<Word>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<WordContext>();
        mockContext.Setup(m => m.Words).Returns(mockSet.Object);

        var sut = new WordRepository(mockContext.Object);
        var words = sut.Get().ToList();
        Assert.NotNull(words);
        Assert.Empty(words);
    }

    [Fact]
    public void Get_Returns_TwoEntries()
    {
        var data = new List<Word>
        {
            new Word("First term"),
            new Word("Second term"),
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Word>>();
        mockSet.As<IQueryable<Word>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<WordContext>();
        mockContext.Setup(m => m.Words).Returns(mockSet.Object);

        var sut = new WordRepository(mockContext.Object);

        var words = sut.Get().ToList();
        Assert.NotNull(words);
        Assert.Equal(2, words.Count);
    }

    [Fact]
    public void Update_SearchCount_NullWord()
    {
        var mockSet = new Mock<DbSet<Word>>();
        var mockContext = new Mock<WordContext>();
        mockContext.Setup(m => m.Words).Returns(mockSet.Object);

        var sut = new WordRepository(mockContext.Object);
        Assert.Throws<NullReferenceException>(() => sut.UpdateSearchCount(null));
    }


    [Fact]
    public void Update_SearchCount_ValidWord()
    {
        var data = new List<Word>
        {
            new Word("First term"),
            new Word("Second term"),
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Word>>();
        mockSet.As<IQueryable<Word>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<WordContext>();
        mockContext.Setup(m => m.Words).Returns(mockSet.Object);

        var word = data.FirstOrDefault();
        Assert.Equal(0, word.SearchCount);

        var sut = new WordRepository(mockContext.Object);
        sut.UpdateSearchCount(word);

        Assert.Equal(1, word.SearchCount);
    }

    [Fact]
    public void Update_SearchTerms_LessThanBefore()
    {
        var builder = new DbContextOptionsBuilder<WordContext>();
        builder.UseInMemoryDatabase("WordDatabase");

        var context = new WordContext(builder.Options);
        context.Words.Add(new Word("First term"));
        context.Words.Add(new Word("Second term"));
        context.Words.Add(new Word("Third term"));
        context.SaveChanges();

        Assert.Equal(3, context.Words.Count());

        var sut = new WordRepository(context);
        sut.Update(new[] { "First term", "A New Term" });

        Assert.Equal(2, context.Words.Count());
        Assert.Collection(context.Words, item => Assert.Equal("First term", item.Term), item => Assert.Equal("A New Term", item.Term));
    }
}
