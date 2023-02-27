using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.StoreDto.ProductDto;
using AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AMH_MarketPlace.Controllers.StoreController.ProductController
{
    [Route("api/product")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest req)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _productService.CreateProduct(req, user);
            CommonResponse<ProductResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Create Product Success",
                Data = result
            };
            return Created("product", response);
        }

        [HttpGet("me"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyProduct([FromQuery] int page, [FromQuery] int size)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _productService.GetMyProduct(user, page, size);
            CommonResponse<PageResponse<ProductResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get All My Product Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllProduct([FromQuery] int page, [FromQuery] int size)
        {
            var result = await _productService.GetAllProduct(page,size);
            CommonResponse<PageResponse<ProductResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get All My Product Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpGet("id"), AllowAnonymous]
        public async Task<IActionResult> GetProductById([FromQuery] string id)
        {
            var result = await _productService.GetProductById(id);

            return Ok(result);
        }

        [HttpDelete, Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteProduct([FromQuery] string id)
        {
            var result = await _productService.DeleteProduct(id);
            CommonResponse<List<string>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Delete Product Success",
                Data = result
            };
            return Ok(response);
        }
    }
}
