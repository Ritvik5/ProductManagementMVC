using Microsoft.EntityFrameworkCore;
using ProductManagement.Repository.Entities;
using ProductManagement.Repository.Interface;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace ProductManagement.Repository.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext context;
        /// <summary>
        /// Addng dependecy
        /// </summary>
        /// <param name="context"> Db Context </param>
        public ProductService(ProductContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Update and insert in database
        /// </summary>
        /// <param name="model"> product model</param>
        /// <returns> bool </returns>
        /// <exception cref="Exception"></exception>
        public bool UpsertProduct(ProductModel model)
        {
            try
            {
                context.Upsert(model.ProductID,model.Code, model.Name, model.Description, model.ExpiryDate, model.Category, model.Images, model.Status);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message) ;
            }
           
        }
        /// <summary>
        /// Get all product list
        /// </summary>
        /// <returns> List Product Model </returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<ProductModel> GetAll()
        {
            try
            {
                // Execute the stored procedure using raw SQL
                var products = context.Product.FromSqlRaw("EXEC SP_fetchProducts").ToList();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Delete Product 
        /// </summary>
        /// <param name="productId"> Product Id </param>
        /// <returns> bool </returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteProduct(int productId)
        {
            try
            {
                context.IsDeleted(productId);
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="productId"> product id</param>
        /// <returns> ProductModel</returns>
        public ProductModel GetById(int productId)
        {
            try
            {
                var result = context.Product.FromSqlRaw("EXEC SP_FetchByProductId @ProductID", new SqlParameter("@ProductID", productId)).AsEnumerable().SingleOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
