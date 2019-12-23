namespace UserService.Dtos.Responses
{
    public class AuthenticatedResponse
    {
        public string Jwt { get; set; }

        public UserResponse User { get; set; }
    }
}