using Address_Book.Models;
using Address_Book.services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Address_Book.Controllers
{
    public class JobController : Controller
    {
        IJobRepository JobService;
        public JobController(IJobRepository _jobRepo)
        {
            JobService = _jobRepo;
        }

        public IActionResult Index()
        {
            List<Job> jobModel = JobService.getAll();
            return View("Index", jobModel);
        }
        public IActionResult add()
        {
            List<Job> jobs = JobService.getAll();
            ViewBag.job = jobs;

            Job job = new Job();
            return View(job);


        }

        [HttpPost]
        public IActionResult saveadd(Job newjob)
        {

            if (ModelState.IsValid == true)
            {

                JobService.create(newjob);
                return RedirectToAction("Index");
            }
            List<Job> jobs = JobService.getAll();
            ViewBag.job = jobs;
            return View("add", newjob);
        }
        public IActionResult edit(int id)
        {
            List<Job> jobs = JobService.getAll();
            ViewBag.job = jobs;

            Job job = JobService.getById(id);
            return View(job);
        }

        public IActionResult saveEdit([FromRoute] int id, Job newjob)
        {
            if (ModelState.IsValid == true)
            {
                JobService.update(id, newjob);
                return RedirectToAction("Index");
            }
            List<Job> jobs = JobService.getAll();
            ViewBag.job = jobs;

            Job job = JobService.getById(id);
            return View("Index", job);

        }

        public IActionResult delete([FromRoute] int id)
        {

            JobService.delete(id);
            return RedirectToAction("Index");

        }
    }
}
