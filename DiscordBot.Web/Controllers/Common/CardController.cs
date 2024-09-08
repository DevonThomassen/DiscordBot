using DiscordBot.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiscordBot.Web.Controllers.Common;

/// <summary>
/// Card controller
/// </summary>
/// <param name="cardService"></param>
[Route("api/[controller]")]
[ApiController]
public class CardController(ICardService cardService) : ControllerBase
{
    private readonly ICardService _cardService =
        cardService ?? throw new ArgumentNullException();

    /// <summary>
    /// Retrieves all card suits represented as strings.
    /// </summary>
    /// <returns></returns>
    [HttpGet("suits")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IEnumerable<string> GetSuits()
    {
        return _cardService.GetSuits();
    }

    /// <summary>
    /// Retrieves all card ranks represented as strings.    
    /// </summary>
    /// <returns></returns>
    [HttpGet("ranks")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IEnumerable<string> GetRanks()
    {
        return _cardService.GetRanks();
    }
}