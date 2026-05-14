using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using System.Linq.Expressions;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class SaleDetailRepository : ISaleDetailRepository
    {

        private readonly Pos2Context _context;

        public SaleDetailRepository(Pos2Context context)
        {
            _context = context;
        }


        // CAMBIA ESTA LÍNEA (Debe devolver BaseEntityResponse, no IEnumerable)
        public async Task<BaseEntityResponse<SaleDetail>> GetSaleDetailBySaleId(int saleId)
        {
            var response = new BaseEntityResponse<SaleDetail>();

            var details = await _context.SaleDetails
                .Include(x => x.Product)
                .Where(x => x.SaleId == saleId)
                .AsNoTracking()
                .ToListAsync();

            response.Items = details;
            response.TotalRecords = details.Count;

            return response;
        }





        public Task<bool> EditAsync(SaleDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SaleDetail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SaleDetail> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
               

        public Task<bool> RegisterAsync(SaleDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }


        public IQueryable<SaleDetail> GetEntityQuery(Expression<Func<SaleDetail, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            throw new NotImplementedException();
        }
    }
}
