using Microsoft.AspNetCore.Mvc;
using DeltaTre.Service.WordApi.Services;

namespace DeltaTre.Service.WordApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordController : ControllerBase
{

    private readonly IWordRepository _wordRepository;
    private readonly ILogger<WordController> _logger;

    public WordController(IWordRepository wordRepository, ILogger<WordController> logger)
    {
        _wordRepository = wordRepository;
        _logger = logger;
    }

    [HttpGet("top/{top}")]
    public ActionResult<IEnumerable<Word>> Top(int top = 5)
    {
        try
        {
            return Ok(_wordRepository.Get()
                .OrderByDescending(word => word.SearchCount)
                .Take(top));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting top {top}");
            return Problem(detail: $"Error getting top {top}");
        }
    }


    [HttpGet("get/{term}")]
    public ActionResult<string> Get(string term)
    {
        try
        {
            var word = _wordRepository.Get(term);
            if (word == null)
            {
                return NotFound();
            }

            _wordRepository.UpdateSearchCount(word);

            return Ok(word.Term);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"Error searching for word {term}");
            return Problem(detail: $"Error searching for word {term}");
        }
    }

    [HttpPut("update")]
    public ActionResult Update(string[] searchTerms)
    {
        if (searchTerms == null)
        {
            return BadRequest();
        }

        try
        {
            _wordRepository.Update(searchTerms);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating word list");
            return Problem(detail: $"Error updating word list");
        }
    }
}

