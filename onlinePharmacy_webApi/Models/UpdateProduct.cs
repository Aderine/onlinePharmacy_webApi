using System.ComponentModel.DataAnnotations;

namespace onlinePharmacy_webApi.Models
{
    public class UpdateProduct
    {
        public string? ProductName { get; set; }
       
        public string? ProductDescription { get; set; }
        
        public string? ProductCategory { get; set; }
        
        public string? Brand { get; set; }
        
        [Range(0, int.MaxValue)]
        public double? Price { get; set; }
        [Range(0, int.MaxValue)]
        public int? QuantityInStock { get; set; }

        public IFormFile? Imagefile { get; set; }
    }
}
