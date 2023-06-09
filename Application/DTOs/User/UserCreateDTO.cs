namespace Application.DTOs.User
{
    public class UserCreateDTO : UserBaseDTO
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public List<Guid>? Roles { get; set; } = new List<Guid> { Guid.Parse("b741c743-4b8d-4627-a98d-c6d9a7cb03af") };


    }
}
