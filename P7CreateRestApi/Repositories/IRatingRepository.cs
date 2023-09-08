using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllRatings();
        Task<Rating> GetRatingByIdAsync(int id);
        Task<int> CreateRatingAsync(Rating rating);
        Task UpdateRatingAsync(Rating rating);
        Task<int> DeleteRatingAsync(int id);
    }
}
