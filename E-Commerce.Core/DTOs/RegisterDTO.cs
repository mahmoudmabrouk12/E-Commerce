namespace E_Commerce.Core.DTOs
{
    public record RegisterDTO : LoginDTO
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }

    }
    public record ActiveAccountDTO
    { 
    public string Token { get; set; }
    public string Email { get; set; }
    

    }
    
}
