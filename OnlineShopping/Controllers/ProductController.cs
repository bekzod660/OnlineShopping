using Application.DTOs.Product;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ProductController : ApiControllerBase<Product>
{
    private readonly IProductRepository _productService;

    public ProductController(IProductRepository productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles = "ProductGet")]
    public async Task<ActionResult<Response<ProductGetDTO>>> GetById(Guid id)
    {
        var mappedProduct = _mapper.Map<ProductGetDTO>(await _productService.GetByIdAsync(id));
        if (mappedProduct != null)
        {
            return Ok(new Response<ProductGetDTO>(true, mappedProduct));
        }
        return BadRequest();

    }
    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles = "ProductGetAll")]
    public async Task<ActionResult<IEnumerable<Response<ProductGetDTO>>>> GetAllProducts(int page = 1, int pageSize = 10)
    {
        IQueryable<Product> Products = await _productService.GetAsync(x => true);

        var products = Products.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

        return Ok(new Response<IEnumerable<ProductGetDTO>>(true, products));
    }

    [HttpPost]
    [Route("[action]")]
    // [Authorize(Roles = "ProductCreate")]
    public async Task<ActionResult<Response<ProductGetDTO>>> Create([FromBody] ProductCreateDTO product)
    {
        Product mappedProduct = _mapper.Map<Product>(product);
        var validation = _validator.Validate(mappedProduct);
        if (validation.IsValid)
        {
            var IsSuccess = await _productService.CreateAsync(mappedProduct);
            return Ok(new Response<ProductGetDTO>(true, IsSuccess));
        }
        return BadRequest();

    }

    [HttpPut]
    [Route("[action]")]
    // [Authorize(Roles = "ProductCreate")]
    public async Task<ActionResult<Response<ProductGetDTO>>> Update([FromBody] ProductUpdateDTO product)
    {
        Product mappedProduct = _mapper.Map<Product>(product);
        var validation = _validator.Validate(mappedProduct);
        if (validation.IsValid)
        {
            var IsSuccess = await _productService.UpdateAsync(mappedProduct);
            return Ok(new Response<ProductGetDTO>(true, IsSuccess));
        }
        return BadRequest();

    }

    [HttpDelete]
    [Route("[action]")]

    public async Task<IActionResult> Delete([FromQuery] Guid Id)
    {
        bool res = await _productService.DeleteAsync(Id);
        if (res) return Ok();
        return BadRequest();

    }
}
