using Address_Book.Models;
using System.Collections.Generic;
using System.Linq;

namespace Address_Book.services
{
    public class DeptRepository : IDeptRepository
    {
        BookEntities context;
        public DeptRepository(BookEntities _context)
        {
            context = _context;
        }

        public int create(Department dept)
        {
            context.department.Add(dept);
            int raw = context.SaveChanges();
            return raw;
        }

        public int delete(int Id)
        {
            Department old = context.department.FirstOrDefault(x => x.Id == Id);
            context.department.Remove(old);
            int raw = context.SaveChanges();
            return raw;
        }

        public List<Department> getAll()
        {
            return context.department.ToList();
        }

        public Department getById(int id)
        {
            return context.department.FirstOrDefault(x => x.Id == id);
        }

        public int update(int id, Department department)
        {
            Department old = context.department.FirstOrDefault(x => x.Id == id);
            old.Name = department.Name;
            old.Manager=department.Manager;
            int raw = context.SaveChanges();
            return raw;
        }
    }
}
