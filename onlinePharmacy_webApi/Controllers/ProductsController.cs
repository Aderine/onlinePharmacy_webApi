using Microsoft.AspNetCore.Mvc;
using onlinePharmacy_webApi.Data;
using onlinePharmacy_webApi.Models;


namespace onlinePharmacy_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string uploadImgFolder;
        private string imgPath = "Uploads/ProductImages";

        public ProductsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;

            // To ensure the directory for image upload is exists
            uploadImgFolder = Path.Combine(_webHostEnvironment.ContentRootPath, imgPath);
            if (!Directory.Exists(uploadImgFolder))
            {
                Directory.CreateDirectory(uploadImgFolder);
            }

        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            if(_db.products == null)
            {
                return NotFound();
            }

            return Ok(_db.products.ToList());
        }

        [HttpGet]
        [Route("/product/{productId:guid}")]
        public IActionResult GetProduct([FromRoute]Guid? productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }

            Product? product = _db.products.Find(productId);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("/createProduct")]
        public IActionResult CreateProduct(CreateProduct product)
        {
            if (ModelState.IsValid)
            {
                if (product == null)
                {
                    return BadRequest();
                }

                Product newProduct = new Product()
                {
                    ProductID = Guid.NewGuid(),
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductCategory = product.ProductCategory,
                    Brand = product.Brand,
                    Price = product.Price,
                    QuantityInStock = product.QuantityInStock
                };

                if (product.Imagefile == null)
                {
                    return BadRequest();
                }else
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.Imagefile.FileName);

                    using (var fileStream = new FileStream(Path.Combine(imgPath, fileName), FileMode.Create))
                    {
                        product.Imagefile.CopyTo(fileStream);
                    }

                    newProduct.ImageUrl = imgPath + "/" + fileName;
                }

                _db.products.Add(newProduct);
                _db.SaveChanges();

                return Ok(newProduct);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPut]
        [Route("/updateProduct/{productId:guid}")]
        
        public IActionResult UpdateProduct([FromRoute] Guid? productId, UpdateProduct? product)
        {
            if(ModelState.IsValid)
            {
                if (productId == null || product == null)
                {
                    return BadRequest();
                }

                Product? productTobeUpdated = _db.products.Find(productId);
                
                if(productTobeUpdated == null)
                {
                    return NotFound();
                }
                else
                {

                    if(product.ProductName != null)
                    {
                        productTobeUpdated.ProductName = product.ProductName;
                    }

                    if(product.ProductDescription != null)
                    {
                        productTobeUpdated.ProductDescription = product.ProductDescription;
                    }
                    
                    if(product.ProductCategory != null)
                    {
                        productTobeUpdated.ProductCategory = product.ProductCategory;
                    }
                    if(product.Brand != null)
                    {
                        productTobeUpdated.Brand = product.Brand;
                    }
                    if(product.Price != null)
                    {
                        productTobeUpdated.Price = product.Price;
                    }
                    if(product.QuantityInStock != null)
                    {
                        productTobeUpdated.QuantityInStock = product.QuantityInStock;
                    }
                    

                    if (product?.Imagefile != null)
                    
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.Imagefile.FileName);

                        using (var fileStream = new FileStream(Path.Combine(imgPath, fileName), FileMode.Create))
                        {
                            product.Imagefile.CopyTo(fileStream);
                        }

                        productTobeUpdated.ImageUrl = imgPath + "/" + fileName;
                    }

                    _db.SaveChanges();
                    
                    return Ok(productTobeUpdated);
                } 
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("deleteProduct/{productId:guid}")]
        public IActionResult DeleteProduct([FromRoute]Guid? productId)
        {
            if(productId == null)
            {
                return BadRequest();
            }

            Product? product = _db.products.Find(productId);
            if(product == null)
            {
                return NotFound();
            }
           
            _db.products.Remove(product);
            _db.SaveChanges();

            return Ok(_db.products.ToList());

        }


    }
}
