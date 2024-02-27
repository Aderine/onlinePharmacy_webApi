using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace onlinePharmacy_webApi.Models
{
    public class CreateProduct
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public string ProductCategory { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
        
        public IFormFile? Imagefile { get; set; }

    }
}
