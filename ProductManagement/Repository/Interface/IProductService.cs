using ProductManagement.Repository.Entities;
using System.Collections.Generic;

namespace ProductManagement.Repository.Interface
{
    public interface IProductService
    {
        bool UpsertProduct(ProductModel model);
        IEnumerable<ProductModel> GetAll();
        bool DeleteProduct(int productId);
        ProductModel GetById(int productId);
    }
}
