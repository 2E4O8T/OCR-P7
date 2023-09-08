using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository
    {
        private readonly PostTradesDbContext _context;

        public TradeRepository(PostTradesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trade>> GetAllTrades()
        {
            return await _context.Trades.ToListAsync();
        }
        public async Task<Trade> GetTradeByIdAsync(int id)
        {
            return await _context.Trades.FindAsync(id);
        }

        public async Task<int> CreateTradeAsync(Trade trade)
        {
            _context.Trades.Add(trade);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateTradeAsync(Trade trade)
        {
            _context.Entry(trade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteTradeAsync(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null)
                return 0;

            _context.Trades.Remove(trade);
            return await _context.SaveChangesAsync();
        }
    }
}
