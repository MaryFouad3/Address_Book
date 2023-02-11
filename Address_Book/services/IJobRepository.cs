using Address_Book.Models;
using System.Collections.Generic;
namespace Address_Book.services
{
    public interface IJobRepository
    {
        int create(Job job);
        int delete(int id);
        List<Job> getAll();
        Job getById(int id);
        int update(int id, Job job);
    }
}
