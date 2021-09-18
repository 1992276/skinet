using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

       

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            List<ProductBrand> productBrands = await this._storeContext.ProductBrands.ToListAsync();

            return productBrands;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await this._storeContext.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .SingleOrDefaultAsync(p=>p.Id.Equals(id));

            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            List<Product> products = await this._storeContext.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .ToListAsync();
           
            return products;
        }

       

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            List<ProductType> productTypes = await this._storeContext.ProductTypes.ToListAsync();

            return productTypes;
        }
    }
}
