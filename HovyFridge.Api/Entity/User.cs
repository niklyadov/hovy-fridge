namespace HovyFridge.Api.Entity
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public UserRole Role { get; set; } = UserRole.Common;
    }
}
