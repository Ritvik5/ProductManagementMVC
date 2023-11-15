using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ProductManagement.Repository.Entities
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "The {0} field is required.")]
        [DisplayName("Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name Should contain minimum 3 characters and Maximum of 50")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product {0} is Required")]
        [StringLength(4000, MinimumLength = 3, ErrorMessage = "Name Should contain minimum 3 characters and Maximum of 4000")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [DisplayName("Expirey Date")]
        public DateTime ExpiryDate { get; set; }
        [Required(ErrorMessage = "Please select {0}")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please select {0}")]
        public string Images { get; set; }
        [Required(ErrorMessage = "Please select {0}.- By default Active")]
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
