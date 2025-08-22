using AutoMapper;
using E_SportsAPP.DTOs.Product;
using E_SportsAPP.Models;
using E_SportsAPP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_SportsAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(_mapper.Map<IEnumerable<ProductResponseDTO>>(products));
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductResponseDTO>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] CreateProductDTO createProduct)
        {
            if (createProduct == null)
            {
                return BadRequest("Product não pode ser nulo.");
            }

            var product = _mapper.Map<Product>(createProduct);
            await _productRepository.AddProductAsync(product);

            var productResponse = _mapper.Map<ProductResponseDTO>(product);
            return CreatedAtAction(nameof(GetProductById), new { id = productResponse.Id }, productResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> UpdateProduct(int id, [FromBody] UpdateProductDTO updateProduct)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _mapper.Map(updateProduct, existingProduct);

            await _productRepository.UpdateProductAsync(id, existingProduct);
            return Ok(_mapper.Map<ProductResponseDTO>(existingProduct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteProductAsync(id);
            return NoContent();
    }   }      
}
