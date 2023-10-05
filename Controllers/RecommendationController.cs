using Microsoft.AspNetCore.Mvc;

namespace bookclub.Controllers;

[ApiController]
[Route("[controller]")]
public class RecommendationController : ControllerBase
{
  private ReviewDBAdapter ReviewAdapter = new ReviewDBAdapter();
  private readonly ILogger<ReviewController> _logger;

  public RecommendationController(ILogger<ReviewController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  public IActionResult GetRecommendationsByUser(string userName)
  {
    return Ok(ReviewAdapter.getRecommendationsByUser(userName).ToList());
  }

}
