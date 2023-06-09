namespace Application.DTOs.User
{
    public class UserUpdateDTO : UserBaseDTO
    {
        public Guid Id { get; set; }
        public string[] Roles { get; set; } = { "user" };
        public string Password { get; set; }

    }
}
