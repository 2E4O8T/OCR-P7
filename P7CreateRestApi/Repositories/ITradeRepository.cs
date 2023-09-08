using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetAllTrades();
        Task<Trade> GetTradeByIdAsync(int id);
        Task<int> CreateTradeAsync(Trade trade);
        Task UpdateTradeAsync(Trade trade);
        Task<int> DeleteTradeAsync(int id);
    }
}
