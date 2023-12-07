namespace JWTDemo002.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Passwordhash { get; set; }

        public static implicit operator UserDto(User user)
        {
            return new()
            {
                Username = user.Username,
                Password = user.Passwordhash,
            };
        }
    }
}
