using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Repository.Entities;
using System;
using System.Collections.Generic;

namespace ProductManagement.Repository
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<ProductModel> Product { get; set; }
        /// <summary>
        /// Update and insert into database
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="code"> code </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description</param>
        /// <param name="expiryDate"> date </param>
        /// <param name="category"> catehgory </param>
        /// <param name="image"> image </param>
        /// <param name="status"> status</param>
        /// <returns> bool </returns>
        /// <exception cref="Exception"></exception>
        public bool Upsert(int id,string code, string name, string description, DateTime expiryDate, string category, string image, string status)
        {
            try
            {
                Database.ExecuteSqlRaw("EXEC SP_UpsertProduct @Id, @Code, @Name, @Description, @ExpiryDate, @Category, @Image, @Status",
                        new SqlParameter("@Id", id),
                        new SqlParameter("@Code", code),
                        new SqlParameter("@Name", name),
                        new SqlParameter("@Description", description),
                        new SqlParameter("@ExpiryDate", expiryDate),
                        new SqlParameter("@Category", category),
                        new SqlParameter("@Image", image),
                        new SqlParameter("@Status", status));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Execute stored procedure for delete
        /// </summary>
        /// <param name="productId">product id </param>
        /// <returns> bool </returns>
        /// <exception cref="Exception"></exception>
        public bool IsDeleted(int productId)
        {
            try
            {
                Database.ExecuteSqlRaw("EXEC SP_DeletePRoduct @ProductID", new SqlParameter("@ProductID", productId));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
