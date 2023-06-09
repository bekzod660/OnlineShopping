using Application.DTOs.User;
using Application.Extension;
using Application.Interfaces;
using Application.Models.Token;
using Application.ResponseModel;
using Domain.Entites.IdentityEntities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers;

[Route("/[controller]")]
[ApiController]
//[Authorize]
public class UserController : ApiControllerBase<User>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _tokenRepository;
    private readonly IJWTManagerRepository jWTManager;
    private readonly IRoleRepository _roleRepository;


    public UserController(IUserRepository userRepository, IRefreshTokenRepository tokenService, IJWTManagerRepository jWTManager, Application.Abstraction.IApplicationDbContext db, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenService;
        this.jWTManager = jWTManager;
        _roleRepository = roleRepository;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public async Task<IActionResult> Login([FromForm] UserLoginDTO user)
    {
        string HashPassword = user.Password.PasswordHash();
        var foundUser = (await _userRepository.GetAsync(x => x.Password.Equals(HashPassword) && x.Email.Equals(user.Email))).ToList();


        if (foundUser.Count == 0)
        {
            return NotFound("User not found!");
        }
        var userr = foundUser.First();
        var token = jWTManager.GenerateToken(userr);
        if (token == null)
        {
            return Unauthorized("Incorrect username or password!");
        }
        var foundRefreshToken = (_tokenRepository.Get(x => x.Email == user.Email));
        if (foundRefreshToken != null)
        {
            foundRefreshToken[0].RefreshToken = token.Refresh_Token;
            foundRefreshToken[0].ExpireDate = DateTime.UtcNow.AddMinutes(2);
            await _tokenRepository.UpdateAsync(foundRefreshToken[0]);
            return Ok(token);
        }
        else
        {
            UserRefreshToken userRefreshTokens = new UserRefreshToken
            {
                RefreshToken = token.Refresh_Token,
                Email = user.Email,
                ExpireDate = DateTime.UtcNow.AddMinutes(3)
            };

            await _tokenRepository.CreateAsync(userRefreshTokens);

            return Ok(token);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("create")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response<UserGetDTO>>> Create([FromForm] UserCreateDTO user)
    {
        User mappedUser = _mapper.Map<User>(user);
        var validationResult = _validator.Validate(mappedUser);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }
        mappedUser.Roles = new List<Role>();
        foreach (Guid item in user.Roles)
        {
            Role? role = await _roleRepository.GetByIdAsync(item);
            if (role != null)
            {
                mappedUser.Roles.Add(role);
            }
            else return BadRequest(new Response<UserGetDTO>(false, $"{item} id not found"));
        }
        mappedUser.Password = mappedUser.Password.PasswordHash();
        await _userRepository.CreateAsync(mappedUser);
        UserGetDTO _user = _mapper.Map<UserGetDTO>(mappedUser);
        return Ok(new Response<UserGetDTO>(true, _user));

    }

    [HttpGet]
    [Route("GetAll")]
    //  [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetAll()
    {
        IEnumerable<User> ress = (await _userRepository.GetAsync(x => true)).AsEnumerable();
        IEnumerable<UserGetDTO> mappedUsers = _mapper.Map<IEnumerable<UserGetDTO>>(ress);
        return Ok(new Response<IEnumerable<UserGetDTO>>(mappedUsers));
    }
    [HttpGet]
    [Route("GetById")]
    public async Task<ActionResult<UserGetDTO>> GetById(Guid id)
    {
        UserGetDTO mappedUsers = _mapper.Map<UserGetDTO>(await _userRepository.GetByIdAsync(id));
        return Ok(new Response<UserGetDTO>(true, mappedUsers));
    }



    [HttpPut]
    [Route("[action]")]
    //[Authorize(Roles = "ProductUpdate")]
    public async Task<ActionResult<Response<UserGetDTO>>> Update([FromBody] UserUpdateDTO entity)
    {
        var mappedUser = _mapper.Map<User>(entity);
        var validationResult = _validator.Validate(mappedUser);
        if (!validationResult.IsValid)
        {
            return BadRequest();
        }
        var result = _userRepository.UpdateAsync(mappedUser);
        UserGetDTO res = _mapper.Map<UserGetDTO>(mappedUser);
        return Ok(new Response<UserGetDTO>(res));
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles = "ProductDelete")]
    public async Task<ActionResult<Response<UserGetDTO>>> Delete([FromQuery] Guid Id)
    {
        var foundUser = _userRepository.GetByIdAsync(Id);
        if (foundUser != null)
        {
            await _userRepository.DeleteAsync(Id);
            return Ok(new Response<object>(true, foundUser));
        }
        return BadRequest();
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(Tokens token)
    {
        UserRefreshToken nt = new UserRefreshToken
        {
            RefreshToken = token.Refresh_Token,

        };
        var foundRefreshToken = (await _tokenRepository.GetAsync(x => x.RefreshToken == token.Refresh_Token)).First();
        if (foundRefreshToken != null)
        {
            if (foundRefreshToken.ExpireDate < DateTime.UtcNow)
            {
                await _tokenRepository.DeleteAsync(foundRefreshToken.Id);
                return BadRequest("Time out Refresh token");
            }
            else
            {
                var res = jWTManager.GenerateToken((await _userRepository.GetAsync(x => x.Email == foundRefreshToken.Email)).First());
                if (res == null)
                {
                    return Unauthorized("Incorrect username or password!");
                }
                foundRefreshToken.RefreshToken = res.Refresh_Token;
                foundRefreshToken.ExpireDate = DateTime.UtcNow.AddMinutes(2);
                await _tokenRepository.UpdateAsync(foundRefreshToken);

                return Ok(res);

            }
        }
        return BadRequest();

    }
    [HttpPost]
    [Route("log")]
    public IActionResult log()
    {
        return Ok();
    }


}
