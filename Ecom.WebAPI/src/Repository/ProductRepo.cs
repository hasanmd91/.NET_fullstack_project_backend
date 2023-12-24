using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecom.WebAPI.src.Repository
{
    public class ProductRepo : IProductRepo
    {

        private readonly DbSet<Product> _products;
        private readonly DataBaseContext _database;

        public ProductRepo(DataBaseContext dataBase)
        {
            _products = dataBase.Product;
            _database = dataBase;
        }

        public async Task<Product> CreateOneProductAsync(Product product)
        {
            await _products.AddAsync(product);
            await _database.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteOneProductAsync(Guid productId)
        {
            var product = await _products.Include(p => p.Images).FirstOrDefaultAsync(u => u.Id == productId);
            if (product != null)
            {
                _database.Images.RemoveRange(product.Images);
                _products.Remove(product);
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync(GetAllParams options)
        {
            var query = _products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(options.Search));

            if (options.CategoryId != Guid.Empty)
            {
                query = query.Where(p => p.Category.Id == options.CategoryId);
            }

            switch (options.SortOrder)
            {
                case "asc":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "dsc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
            }

            var products = await query
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();

            return products;
        }



        public async Task<Product> GetOneProductByIdAsync(Guid productId)
        {
            var product = await _products
                                    .Include(p => p.Images)
                                    .Include(p => p.Category)
                                    .Include(p => p.Reviews)
                                    .ThenInclude(p => p.User)
                                    .FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<Product> UpdateOneProductAsync(Guid productId, Product updatedProduct)
        {
            var existingProduct = await _products
                                        .Include(p => p.Images)
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(u => u.Id == productId);

            if (existingProduct is not null)
            {

                existingProduct.Title = updatedProduct.Title ?? existingProduct.Title;
                existingProduct.Description = updatedProduct.Description ?? existingProduct.Description;

                if (updatedProduct.Price != default)
                {
                    existingProduct.Price = updatedProduct.Price;
                }

                if (updatedProduct.Quantity != default)
                {
                    existingProduct.Quantity = updatedProduct.Quantity;
                }
                existingProduct.Images = updatedProduct.Images ?? existingProduct.Images;

                _database.Update(existingProduct);
                await _database.SaveChangesAsync();
            }

            return existingProduct;
        }
    }
}