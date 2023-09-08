using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePoint>> GetAllCurvePoints();
        Task<CurvePoint> GetCurvePointByIdAsync(int id);
        Task<int> CreateCurvePointAsync(CurvePoint curvePoint);
        Task UpdateCurvePointAsync(CurvePoint curvePoint);
        Task<int> DeleteCurvePointAsync(int id);
    }
}
