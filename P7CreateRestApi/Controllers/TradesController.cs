using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;

        public TradesController(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        // GET: api/Trades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetTrades()
        {
            var trades = await _tradeRepository.GetAllTrades();
            if (trades == null)
            {
                return NotFound();
            }
            return Ok(trades);
        }

        // GET: api/Trades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> GetTrade(int id)
        {
            var trade = await _tradeRepository.GetTradeByIdAsync(id);

            if (trade == null)
            {
                return NotFound();
            }

            return Ok(trade);
        }

        // PUT: api/Trades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrade(int id, Trade trade)
        {
            if (id != trade.TradeId)
            {
                return BadRequest();
            }

            await _tradeRepository.UpdateTradeAsync(trade);

            return NoContent();
        }

        // POST: api/Trades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trade>> PostTrade(Trade trade)
        {
            if (trade == null)
            {
                return BadRequest();
            }

            await _tradeRepository.CreateTradeAsync(trade);

            return CreatedAtAction("GetTrade", new { id = trade.TradeId }, trade);
        }

        // DELETE: api/Trades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            var result = await _tradeRepository.DeleteTradeAsync(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
