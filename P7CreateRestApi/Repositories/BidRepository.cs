using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly PostTradesDbContext _context;

        public BidRepository(PostTradesDbContext context)
        {
            _context = context;
        }

        public async Task<Bid> GetBidByIdAsync(int id)
        {
            return await _context.Bids.FindAsync(id);
        }

        public async Task<int> CreateBidAsync(Bid bid)
        {
            _context.Bids.Add(bid);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateBidAsync(Bid bid)
        {
            _context.Entry(bid).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteBidAsync(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
                return 0;

            _context.Bids.Remove(bid);
            return await _context.SaveChangesAsync();
        }
    }
}
