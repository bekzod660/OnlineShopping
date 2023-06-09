using Application.DTOs.Brand;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandContoller : ApiControllerBase<Brand>
    {
        private readonly IBrandRepository _brandRepository;

        public BrandContoller(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Response<BrandGetDTO>>> Create([FromForm] BrandCreateDTO brand)
        {
            Brand mappedBrand = _mapper.Map<Brand>(brand);
            await _brandRepository.CreateAsync(mappedBrand);
            BrandGetDTO getBrand = _mapper.Map<BrandGetDTO>(mappedBrand);
            return Ok(new Response<BrandGetDTO>(getBrand));
        }

    }


}
