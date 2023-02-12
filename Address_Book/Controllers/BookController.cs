using Address_Book.Models;
using Address_Book.services;
using ClosedXML.Excel;
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
   
        public IActionResult Index(string SearchText = "")
        {
            List<Book> bookModel;
            if(SearchText != "" && SearchText != null)
            {
                bookModel = BookService.getAll()
                    .Where(b => b.FullName.Contains(SearchText)
                    || b.Address.Contains(SearchText) 
                    || b.Email.Contains(SearchText)).ToList();

            }
            else
            bookModel = BookService.getAll();
            return View("Index",bookModel);
        }

        public IActionResult ExportToExcel()
        {
            List<Book> bookModel = BookService.getAll();
            using (var workbook = new XLWorkbook())
            {
                var worksheet=workbook.Worksheets.Add("bookModel");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "id";
                worksheet.Cell(currentRow, 2).Value = "FullName";
                worksheet.Cell(currentRow, 3).Value = "JobTitle";
                worksheet.Cell(currentRow, 4).Value = "DepartmentName";
                worksheet.Cell(currentRow, 5).Value = "MobileNo";
                worksheet.Cell(currentRow, 6).Value = "HomeTelNo";
                worksheet.Cell(currentRow, 7).Value = "Dateofbirth";
                worksheet.Cell(currentRow, 8).Value = "Address";
                worksheet.Cell(currentRow, 9).Value = "Email";

                foreach (var book in bookModel)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = book.id;
                    worksheet.Cell(currentRow, 2).Value = book.FullName;
                    worksheet.Cell(currentRow, 3).Value = book.JobTitle;
                    worksheet.Cell(currentRow, 4).Value = book.DepartmentName;
                    worksheet.Cell(currentRow, 5).Value = book.MobileNo;
                    worksheet.Cell(currentRow, 6).Value = book.HomeTelNo;
                    worksheet.Cell(currentRow, 7).Value = book.Dateofbirth;
                    worksheet.Cell(currentRow, 8).Value = book.Address;
                    worksheet.Cell(currentRow, 9).Value = book.Email;
                  
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "bookModel.xlsx");
                }

            }
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
