using Address_Book.Models;
using System.Collections.Generic;
namespace Address_Book.services
{
    public interface IDeptRepository
    {
        int create(Department dept);
        int delete(int id);
        List<Department> getAll();
        Department getById(int id);
        int update(int id, Department dept);
    }
}
