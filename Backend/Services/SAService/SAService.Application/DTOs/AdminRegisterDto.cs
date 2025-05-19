namespace SAService.Application.DTOs
{
    public class AdminRegisterDto : RegisterUserDto
    {
        public required string Role { get; set; }
    }
}
