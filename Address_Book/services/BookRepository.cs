using Address_Book.Models;
using System.Collections.Generic;
using System.Linq;
namespace Address_Book.services
{
    public class BookRepository:IBookRepository
    {
        BookEntities context; 
        public BookRepository(BookEntities _context)
        {
            context = _context;
        }

        public int create(Book book)
        {
            context.book.Add(book);
            int raw = context.SaveChanges();
            return raw;
        }

        public int delete(int id)
        {
            Book old = context.book.FirstOrDefault(x => x.id == id);
            context.book.Remove(old);
            int raw = context.SaveChanges();
            return raw;
        }

        public List<Book> getAll()
        {
            return context.book.ToList();
        }

        public Book getById(int id)
        {
            return context.book.FirstOrDefault(x => x.id == id);
        }

        public int update(int id, Book book)
        {
            Book old = context.book.FirstOrDefault(x => x.id == id);
            old.FullName = book.FullName;
            old.JobTitle = book.JobTitle;
            old.DepartmentName = book.DepartmentName;
            old.MobileNo = book.MobileNo;
            old.HomeTelNo=book.HomeTelNo;
            old.Dateofbirth = book.Dateofbirth;
            old.Address = book.Address;
            old.Email = book.Email;
            old.Photo = book.Photo;
            int raw = context.SaveChanges();
            return raw;
        }
    }
}
