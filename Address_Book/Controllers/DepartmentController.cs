using Address_Book.Models;
using Address_Book.services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Address_Book.Controllers
{
    public class DepartmentController : Controller
    {
        IDeptRepository DeptService;
        public DepartmentController(IDeptRepository _deptRepo)
        {
            DeptService = _deptRepo;
        }

        public IActionResult Index()
        {
            List<Department> deptModel = DeptService.getAll();
            return View("Index", deptModel);
        }
        public IActionResult add()
        {
            List<Department> depts = DeptService.getAll();
            ViewBag.department = depts;

            Department dept = new Department();
            return View(dept);


        }

        [HttpPost]
        public IActionResult saveadd(Department newdept)
        {

            if (ModelState.IsValid == true)
            {

                DeptService.create(newdept);
                return RedirectToAction("Index");
            }
            List<Department> depts = DeptService.getAll();
            ViewBag.department = depts;
            return View("add", newdept);
        }
        public IActionResult edit(int id)
        {
            List<Department> depts = DeptService.getAll();
            ViewBag.department = depts;

            Department dept = DeptService.getById(id);
            return View(dept);
        }

        public IActionResult saveEdit([FromRoute] int id, Department newdept)
        {
            if (ModelState.IsValid == true)
            {
                Department dep = DeptService.getById(id);
                DeptService.update(id, newdept);
                return RedirectToAction("Index");
            }
            List<Department> depts = DeptService.getAll();
            ViewBag.department = depts;

            Department dept = DeptService.getById(id);
            return View("Index", dept);

        }

        public IActionResult delete([FromRoute] int Id)
        {

            DeptService.delete(Id);
            return RedirectToAction("Index");

        }
    }
}
