namespace JWT.EF
{
    public class Role 
    {
        public static readonly Role Registerd = new Role(1, "Registerd");
        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<User> Users { get; set;} 
    }
}
