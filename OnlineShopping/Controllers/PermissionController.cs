using Application.DTOs.Permission;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entites.IdentityEntities;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class PermissionController : ApiControllerBase<Permission>
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionController(IPermissionRepository permissionService)
    {
        _permissionRepository = permissionService;
    }

    [HttpPost]
    //[ActionModelValidation]
    [Route("Create")]
    public async Task<ActionResult<Response<PermissionCreateDTO>>> Create([FromBody] PermissionCreateDTO permissions)
    {
        Permission mappedPermisson = _mapper.Map<Permission>(permissions);
        //  var validationResult = _validator.Validate(mappedPermisson);

        if (!ModelState.IsValid)
        {
            return BadRequest(new Response<PermissionGetDTO>());
        }
        await _permissionRepository.CreateAsync(mappedPermisson);
        return Ok(new Response<object>(mappedPermisson));
    }
}
