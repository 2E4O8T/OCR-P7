using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly PostTradesDbContext _context;

        public CurvePointRepository(PostTradesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurvePoint>> GetAllCurvePoints()
        {
            return await _context.CurvePoints.ToListAsync();
        }
        public async Task<CurvePoint> GetCurvePointByIdAsync(int id)
        {
            return await _context.CurvePoints.FindAsync(id);
        }

        public async Task<int> CreateCurvePointAsync(CurvePoint curvePoint)
        {
            _context.CurvePoints.Add(curvePoint);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateCurvePointAsync(CurvePoint curvePoint)
        {
            _context.Entry(curvePoint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCurvePointAsync(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null)
                return 0;

            _context.CurvePoints.Remove(curvePoint);
            return await _context.SaveChangesAsync();
        }
    }
}
