using Microsoft.AspNetCore.Mvc;

namespace bookclub.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
  private ReviewDBAdapter ReviewAdapter = new ReviewDBAdapter();
  private readonly ILogger<ReviewController> _logger;

  public ReviewController(ILogger<ReviewController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  public IActionResult GetReviewsByReviewer(string reviewerName)
  {
    return Ok(ReviewAdapter.getReviewsByReviewer(reviewerName).ToList());
  }

  [HttpPost]
  [ActionName(nameof(Review))]
  public ActionResult<Review> CreateReview(Review newReview)
  {
    var res = ReviewAdapter.createNewReview(newReview);
    return CreatedAtAction(nameof(Review), new { BookTitle = newReview.BookTitle }, newReview);
  }
}
