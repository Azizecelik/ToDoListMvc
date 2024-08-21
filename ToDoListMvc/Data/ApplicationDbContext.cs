using Microsoft.EntityFrameworkCore;
using ToDoListProject.Models;

namespace ToDoListProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        // DbContext constructor'ı, DbContextOptions türünde bir parametre alır ve bu parametreyi base (DbContext) constructor'ına iletir.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Gorevler isimli DbSet, veritabanındaki GorevlerInfo tabloları ile iletişim kurmak için kullanılır.
        public DbSet<GorevlerInfo> Gorevler { get; set; }
    }
}
