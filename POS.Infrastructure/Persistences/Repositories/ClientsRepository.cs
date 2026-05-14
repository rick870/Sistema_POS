using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class ClientsRepository : GenericRepository<Client>, IClientsRepository
    {
        public ClientsRepository(Pos2Context context) : base(context) { }

        public async Task<BaseEntityResponse<Client>> ListClients(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Client>();

            // Filtramos por registros no eliminados lógicamente e incluimos el tipo de documento
            var clients = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null)
                .Include(x => x.DocumentType)
                .AsNoTracking();

            // Filtros de texto (Nombre, Email, Documento)
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        clients = clients.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        clients = clients.Where(x => x.Email!.Contains(filters.TextFilter));
                        break;
                    case 3:
                        clients = clients.Where(x => x.DocumentNumber!.Contains(filters.TextFilter));
                        break;
                }
            }

            // Filtro por estado (Activo/Inactivo)
            if (filters.StateFilter is not null)
            {
                clients = clients.Where(x => x.State.Equals(filters.StateFilter));
            }

            // Filtro por rango de fechas de creación
            if (filters.StartDate is not null && filters.EndDate is not null)
            {
                clients = clients.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) &&
                                             x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            // Ordenamiento por defecto
            if (string.IsNullOrEmpty(filters.Sort)) filters.Sort = "Id";

            // Respuesta paginada
            response.TotalRecords = await clients.CountAsync();
            response.Items = await Ordering(filters, clients, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    
    
    }
}
