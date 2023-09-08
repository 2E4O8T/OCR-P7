using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository
    {
        private readonly PostTradesDbContext _context;

        public RatingRepository(PostTradesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllRatings()
        {
            return await _context.Ratings.ToListAsync();
        }
        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<int> CreateRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            _context.Entry(rating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRatingAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
                return 0;

            _context.Ratings.Remove(rating);
            return await _context.SaveChangesAsync();
        }
    }
}
