using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingsController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            var ratings = await _ratingRepository.GetAllRatings();
            if (ratings == null)
            {
                return NotFound();
            }
            return Ok(ratings);
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _ratingRepository.GetRatingByIdAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            if (id != rating.RatingId)
            {
                return BadRequest();
            }

            await _ratingRepository.UpdateRatingAsync(rating);

            return NoContent();
        }

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
            if (rating == null)
            {
                return BadRequest();
            }

            await _ratingRepository.CreateRatingAsync(rating);

            return CreatedAtAction("GetRating", new { id = rating.RatingId }, rating);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var result = await _ratingRepository.DeleteRatingAsync(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
