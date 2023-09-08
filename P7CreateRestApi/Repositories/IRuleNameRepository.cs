using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IRuleNameRepository
    {
        Task<IEnumerable<RuleName>> GetAllRuleNames();
        Task<RuleName> GetRuleNameByIdAsync(int id);
        Task<int> CreateRuleNameAsync(RuleName ruleName);
        Task UpdateRuleNameAsync(RuleName ruleName);
        Task<int> DeleteRuleNameAsync(int id);
    }
}
