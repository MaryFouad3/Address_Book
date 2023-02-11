using Address_Book.Models;
using System.Collections.Generic;
using System.Linq;

namespace Address_Book.services
{
    public class JobRepository:IJobRepository
    {
        BookEntities context;
        public JobRepository(BookEntities _context)
        {
            context = _context;
        }

        public int create(Job job)
        {
            context.job.Add(job);
            int raw = context.SaveChanges();
            return raw;
        }

        public int delete(int id)
        {
            Job old = context.job.FirstOrDefault(x => x.Id == id);
            context.job.Remove(old);
            int raw = context.SaveChanges();
            return raw;
        }

        public List<Job> getAll()
        {
            return context.job.ToList();
        }

        public Job getById(int id)
        {
            return context.job.FirstOrDefault(x => x.Id == id);
        }

        public int update(int id, Job job)
        {
            Job old = context.job.FirstOrDefault(x => x.Id == id);
            old.Name = job.Name;  
            int raw = context.SaveChanges();
            return raw;
        }
    }

}
