using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IBidRepository
    {
        Task<IEnumerable<Bid>> GetAllBids();
        Task<Bid> GetBidByIdAsync(int id);
        Task<int> CreateBidAsync(Bid bid);
        Task UpdateBidAsync(Bid bid);
        Task<int> DeleteBidAsync(int id);
    }
}
