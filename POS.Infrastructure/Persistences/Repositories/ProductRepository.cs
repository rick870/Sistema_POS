using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(Pos2Context context) : base(context) { }


        public async Task<BaseEntityResponse<Product>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Product>();

            // 1. Consulta base filtrando eliminados y cargando relaciones
            var products = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null)
                .Include(x => x.Category) // Importante para ver el nombre de la categoría
                .Include(x => x.Provider) // Importante para ver quién lo provee
                .AsNoTracking();

            // 2. Filtros por texto (Nombre o Código)
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1: // Filtrar por Nombre del producto
                        products = products.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2: // Filtrar por Código (SKU/Barras)
                        products = products.Where(x => x.Code!.Contains(filters.TextFilter));
                        break;
                }
            }

            // 3. Filtro por Estado (Activo/Inactivo)
            if (filters.StateFilter is not null)
            {
                products = products.Where(x => x.State.Equals(filters.StateFilter));
            }

            // 4. Filtro por rango de fechas de creación
            if (filters.StartDate is not null && filters.EndDate is not null)
            {
                products = products.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) &&
                                             x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            // 5. Ordenación por defecto
            if (filters.Sort is null) filters.Sort = "Id";

            // 6. Respuesta paginada
            response.TotalRecords = await products.CountAsync();
            response.Items = await Ordering(filters, products, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}
