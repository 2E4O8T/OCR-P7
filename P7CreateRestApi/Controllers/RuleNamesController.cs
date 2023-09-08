using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleNamesController : ControllerBase
    {
        private readonly IRuleNameRepository _ruleNameRepository;

        public RuleNamesController(IRuleNameRepository ruleNameRepository)
        {
            _ruleNameRepository = ruleNameRepository;
        }

        // GET: api/RuleNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetRuleNames()
        {
            var ruleNames = await _ruleNameRepository.GetAllRuleNames();
            if (ruleNames == null)
            {
                return NotFound();
            }
            return Ok(ruleNames);
        }

        // GET: api/RuleNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleName>> GetRuleName(int id)
        {
            var ruleName = await _ruleNameRepository.GetRuleNameByIdAsync(id);

            if (ruleName == null)
            {
                return NotFound();
            }

            return Ok(ruleName);
        }

        // PUT: api/RuleNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuleName(int id, RuleName ruleName)
        {
            if (id != ruleName.RuleNameId)
            {
                return BadRequest();
            }

            await _ruleNameRepository.UpdateRuleNameAsync(ruleName);

            return NoContent();
        }

        // POST: api/RuleNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleName>> PostRuleName(RuleName ruleName)
        {
            if (ruleName == null)
            {
                return BadRequest();
            }

            await _ruleNameRepository.CreateRuleNameAsync(ruleName);

            return CreatedAtAction("GetRuleName", new { id = ruleName.RuleNameId }, ruleName);
        }

        // DELETE: api/RuleNames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            var result = await _ruleNameRepository.DeleteRuleNameAsync(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
