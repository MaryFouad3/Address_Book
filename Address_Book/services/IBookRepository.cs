using Address_Book.Models;
using System.Collections.Generic;

namespace Address_Book.services
{
    public interface IBookRepository
    {
        int create(Book book);
        int delete(int id);
        List<Book> getAll();
        Book getById(int id);
        int update(int id, Book book);
    }
}
