using JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.EF
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Course> Courses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasMany(x => x.Permissions)
                        .WithMany().UsingEntity<RolePermission>();
            modelBuilder.Entity<Role>().HasMany(x => x.Users).WithMany();
            modelBuilder.Entity<Role>().HasData(Role.Registerd);

            modelBuilder.Entity<Permission>().HasKey(x => x.Id);

            IEnumerable<Permission> permissions = Enum.GetValues<Models.Permission>()
                .Select(x => new Permission { Id = (int)x, Name = x.ToString() });

            modelBuilder.Entity<Permission>().HasData(permissions);

            modelBuilder.Entity<RolePermission>().HasKey(x => new {x.RoleId,x.PermissionId});
            RolePermission rolePermssion = new RolePermission();

            rolePermssion.RoleId = Role.Registerd.Id;
            rolePermssion.PermissionId = (int)Models.Permission.AccessCourse;
            modelBuilder.Entity<RolePermission>().HasData(rolePermssion);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
