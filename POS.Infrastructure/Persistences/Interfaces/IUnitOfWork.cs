using POS.Infrastructure.FileStorage;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Declaración o matrícula de nuestras interfaces a nivel de repository
        ICategoryRepository Category { get; }

        IUserRepository User { get; }

        IAzureStorage Storage { get; }

        IProviderRepository Provider { get; }

        IProductRepository Product { get; }

        IClientsRepository Client { get; }

        ISaleRepository Sale { get; }

        ISaleDetailRepository SaleDetail { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}