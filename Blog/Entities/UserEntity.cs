namespace Blog.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
    }
}