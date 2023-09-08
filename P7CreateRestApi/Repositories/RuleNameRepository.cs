using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class RuleNameRepository
    {
        private readonly PostTradesDbContext _context;

        public RuleNameRepository(PostTradesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RuleName>> GetAllRuleNames()
        {
            return await _context.RuleNames.ToListAsync();
        }
        public async Task<RuleName> GetRuleNameByIdAsync(int id)
        {
            return await _context.RuleNames.FindAsync(id);
        }

        public async Task<int> CreateRuleNameAsync(RuleName ruleName)
        {
            _context.RuleNames.Add(ruleName);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateRuleNameAsync(RuleName ruleName)
        {
            _context.Entry(ruleName).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRuleNameAsync(int id)
        {
            var ruleName = await _context.RuleNames.FindAsync(id);
            if (ruleName == null)
                return 0;

            _context.RuleNames.Remove(ruleName);
            return await _context.SaveChangesAsync();
        }
    }
}
