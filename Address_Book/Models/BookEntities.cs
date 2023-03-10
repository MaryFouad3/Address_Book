using Microsoft.EntityFrameworkCore;

namespace Address_Book.Models
{
    public class BookEntities: DbContext
    {
        public BookEntities()
        {

        }
        public BookEntities(DbContextOptions<BookEntities> options) : base(options)
        {

        }
        public DbSet<Book> book { get; set; }
        public DbSet<Job> job { get; set; }
        public DbSet<Department> department { get; set; }
       
        
    }
}
