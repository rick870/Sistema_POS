using Microsoft.Extensions.Configuration;
using POS.Infrastructure.FileStorage;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Pos2Context _context;
        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }

        public IAzureStorage Storage { get; private set; }

        public IProviderRepository Provider { get; private set; }

        public IProductRepository Product  { get; private set; }

        public IClientsRepository Client { get; private set; }

        public ISaleRepository Sale  { get; private set; }

        public ISaleDetailRepository SaleDetail { get; private set; }

        public UnitOfWork(Pos2Context context, IConfiguration configuration )
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
            Storage = new AzureStorage(configuration);
            Provider = new ProviderRepository(_context);
            Product = new ProductRepository(_context);
            Client = new ClientsRepository(_context);
            Sale = new SaleRepository(_context);
            SaleDetail = new SaleDetailRepository(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}