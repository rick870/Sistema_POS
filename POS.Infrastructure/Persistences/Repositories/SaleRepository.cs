using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(Pos2Context context) : base(context) { }

       

        public async Task<BaseEntityResponse<Sale>> ListSales(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Sale>();
                       
            // Filtramos registros no eliminados lógicamente
            var sales = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null)
                .Include(x => x.Client)
                .Include(x => x.VoucherDocumentType)
                .AsNoTracking();

            // 1. Filtros de texto (Comprobante o Cliente)
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        sales = sales.Where(x => x.VoucherNumber.Contains(filters.TextFilter));
                        break;
                    case 2:
                        sales = sales.Where(x => x.Client.Name.Contains(filters.TextFilter));
                        break;
                }
            }

            // 2. Filtro por estado
            if (filters.StateFilter is not null)
            {
                sales = sales.Where(x => x.State.Equals(filters.StateFilter));
            }

            // 3. Filtro por rango de fechas (Copiado de tu lógica de clientes)
            if (filters.StartDate is not null && filters.EndDate is not null)
            {
                sales = sales.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) &&
                                         x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            // 4. Ordenamiento por defecto
            if (string.IsNullOrEmpty(filters.Sort)) filters.Sort = "Id";

            // 5. Respuesta paginada usando tus métodos de ayuda
            response.TotalRecords = await sales.CountAsync();
            response.Items = await Ordering(filters, sales, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<Sale?> GetSaleById(int saleId)
        {
            return await _context.Sales
                .Include(x => x.Client)
                .Include(x => x.SaleDetails) 
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == saleId);
        }
    }
}
