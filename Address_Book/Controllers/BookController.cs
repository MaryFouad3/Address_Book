using Address_Book.Models;
using Address_Book.services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book.Controllers
{
    public class BookController : Controller
    {
        readonly IBookRepository  BookService;
        readonly IJobRepository JobRepository;
        readonly IDeptRepository DeptRepository;
        public BookController(IBookRepository _bookRepo,
            IJobRepository _jobRepository,
            IDeptRepository _deptRepository)
        {
            BookService = _bookRepo;
            JobRepository = _jobRepository;
            DeptRepository = _deptRepository;
        }
   
        public IActionResult Index()
        {
            List<Book> bookModel = BookService.getAll();
            return View("Index",bookModel);
        }
        public IActionResult add()
        {
            ViewBag.Jobs = JobRepository.getAll();
            ViewBag.Depts = DeptRepository.getAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> saveadd(Book newbook)
        {
            if (ModelState.IsValid == true)
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files.First();
                    // 5242880 : Number Of bytes: Max Image Size is 5mb
                    if (file.Length > 5242880)
                    {
                        ModelState.AddModelError("", "File Is bigger than 5MB");
                        return View(newbook);
                    }
                    using var DataStream = new MemoryStream();
                    await file.CopyToAsync(DataStream);
                    newbook.Photo = DataStream.ToArray();
                }

                BookService.create(newbook);
                return RedirectToAction("Index");
            }
            ViewBag.Jobs = JobRepository.getAll();
            ViewBag.Depts = DeptRepository.getAll();
            return View(newbook);
        }
        public IActionResult edit(int id)
        {
            List<Book> books = BookService.getAll();
            ViewBag.book = books;
            Book book = BookService.getById(id);
            ViewBag.Jobs = JobRepository.getAll();
            ViewBag.Depts = DeptRepository.getAll();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> saveEdit([FromRoute] int id, Book newbook)
        {
            if (ModelState.IsValid == true)
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files.First();
                    // 5242880 : Number Of bytes: Max Image Size is 5mb
                    if (file.Length > 5242880)
                    {
                        ModelState.AddModelError("", "File Is bigger than 5MB");
                        return View(newbook);
                    }
                    using var DataStream = new MemoryStream();
                    await file.CopyToAsync(DataStream);
                    newbook.Photo = DataStream.ToArray();
                }

                BookService.update(id, newbook);
                return RedirectToAction("Index");
            }
            ViewBag.Jobs = JobRepository.getAll();
            ViewBag.Depts = DeptRepository.getAll();
            List<Book> books = BookService.getAll();
            ViewBag.book = books;

            Book book = BookService.getById(id);
            return View("Index", book);

        }

        public IActionResult delete([FromRoute] int id)
        {
                BookService.delete(id);
                return RedirectToAction("Index");
        }

    }
}
