using Application.DTOs.Role;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entites.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ApiControllerBase<Role>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public RoleController(IRoleRepository roleService, IPermissionRepository permissionRepository)
    {
        Console.WriteLine("Scoped");
        _roleRepository = roleService;
        _permissionRepository = permissionRepository;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<Response<RoleGetDTO>>> Create([FromBody] RoleCreateDTO role)
    {
        Role mappedRole = _mapper.Map<Role>(role);
        var validationResult = _validator.Validate(mappedRole);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<RoleGetDTO> { Errors = validationResult.Errors, IsSuccess = false });
        }
        mappedRole.Permissions = new List<Permission>();
        foreach (var item in role.Permissions)
        {
            Permission? permission = await _permissionRepository.GetByIdAsync(item);
            if (permission != null)
            {
                mappedRole.Permissions.Add(permission);
            }
            else return BadRequest(new Response<RoleGetDTO>(false, $"{item} id not found"));
        }
        await _roleRepository.CreateAsync(mappedRole);
        RoleGetDTO r = _mapper.Map<RoleGetDTO>(mappedRole);
        return Ok(new Response<RoleGetDTO>(r));

    }
    [HttpGet]
    [Route("GetAll")]
    // [MyAuthorize(claims: new[] { "issuer", "Email" })]
    public async Task<ActionResult<IEnumerable<Response<RoleGetDTO>>>> GetAll()
    {
        IEnumerable<Role> roles = (await _roleRepository.GetAsync(x => true)).AsEnumerable();
        IEnumerable<RoleGetDTO> mappedRoles = _mapper.Map<IEnumerable<RoleGetDTO>>(roles);
        return Ok(new Response<IEnumerable<RoleGetDTO>>(mappedRoles));
    }


    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult<Response<RoleGetDTO>>> GetById(Guid id)
    {
        Role role = (await _roleRepository.GetByIdAsync(id));
        RoleGetDTO mappedRole = _mapper.Map<RoleGetDTO>(role);
        return Ok(new Response<RoleGetDTO>(mappedRole));
    }

    [HttpPut]
    [Route("[action]")]
    // [Authorize(Roles = "ProductUpdate")]
    public async Task<ActionResult<Response<RoleGetDTO>>> Update([FromBody] RoleUpdateDTO entity)
    {
        Role mappedRole = _mapper.Map<Role>(entity);
        var validationResult = _validator.Validate(mappedRole);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object> { Errors = validationResult.Errors, IsSuccess = false });
        }
        mappedRole.Permissions = new List<Permission>();
        foreach (var item in entity.Permissions)
        {
            Permission? permission = await _permissionRepository.GetByIdAsync(item);
            if (permission != null)
            {
                mappedRole.Permissions.Add(permission);
            }
            else return BadRequest(new Response<Role>(false, $"{item} id not found"));
        }
        await _roleRepository.UpdateAsync(mappedRole);
        RoleGetDTO r = _mapper.Map<RoleGetDTO>(mappedRole);
        return Ok(new Response<RoleGetDTO>(r));
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles = "ProductDelete")]
    public async Task<ActionResult<Response<RoleGetDTO>>> Delete([FromQuery] Guid Id)
    {
        return await _roleRepository.DeleteAsync(Id) ? Ok(new Response<bool>(true)) : BadRequest(new Response<bool>(false));
    }
}