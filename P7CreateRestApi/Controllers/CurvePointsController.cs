using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurvePointsController : ControllerBase
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointsController(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        // GET: api/CurvePoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetCurvePoints()
        {
            var curvePoints = await _curvePointRepository.GetAllCurvePoints();
            if (curvePoints == null)
            {
                return NotFound();
            }
            return Ok(curvePoints);
        }

        // GET: api/CurvePoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurvePoint>> GetCurvePoint(int id)
        {
            var curvePoint = await _curvePointRepository.GetCurvePointByIdAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            return Ok(curvePoint);
        }

        // PUT: api/CurvePoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurvePoint(int id, CurvePoint curvePoint)
        {
            if (id != curvePoint.CurvePointId)
            {
                return BadRequest();
            }

            await _curvePointRepository.UpdateCurvePointAsync(curvePoint);

            return NoContent();
        }

        // POST: api/CurvePoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CurvePoint>> PostCurvePoint(CurvePoint curvePoint)
        {
            if (curvePoint == null)
            {
                return BadRequest();
            }

            await _curvePointRepository.CreateCurvePointAsync(curvePoint);

            return CreatedAtAction("GetCurvePoint", new { id = curvePoint.CurvePointId }, curvePoint);
        }

        // DELETE: api/CurvePoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            var result = await _curvePointRepository.DeleteCurvePointAsync(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

