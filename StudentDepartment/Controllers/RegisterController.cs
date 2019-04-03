using StudentDepartment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace StudentDepartment.Controllers
{
    public class RegisterController : Controller
    {
		StudentDepartmentEntities db = new StudentDepartmentEntities();
        private readonly IHostingEnvironment _hostingEnvironment;
		// GET: Register
		public RegisterController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}
		public ActionResult SetDataInDataBase()
        {

            return View();
        }
		[HttpPost]
		public ActionResult SetDataInDataBase(CreateStuAndDep model, IFormFile FileUrl )
		{
			CreateStuAndDep tbl = new CreateStuAndDep();
			
			tbl.StuName = model.StuName;
			tbl.DepName = model.DepName;
			string dirPath = Path.Combine(_hostingEnvironment.WebRootPath, @"uploads\");
			var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + FileUrl.FileName;
			using (var fileStream = new FileStream(dirPath + fileName, FileMode.Create))
			{
				 FileUrl.CopyTo(fileStream);
			}

			tbl.imageURL = fileName;
			tbl.CV = model.CV;
			db.CreateStuAndDep.Add(tbl);

			db.SaveChanges();
			return View();
		}
		public ActionResult ShowDataBaseForUser()
		{
			var item = db.CreateStuAndDep.ToList();
			return View(item);
		}
		public ActionResult Delete(int id)
		{
			var item = db.CreateStuAndDep.Where(x=>x.ID==id).First();
			db.CreateStuAndDep.Remove(item);
			db.SaveChanges();
			var item2 = db.CreateStuAndDep.ToList();
			return View("ShowDataBaseForUser",item2);
		}
		public ActionResult Edit(int id)
		{
			var item = db.CreateStuAndDep.Where(x => x.ID == id).First();
			return View();
		}
		[HttpPost]
		public ActionResult Edit(CreateStuAndDep model)
		{
			var item = db.CreateStuAndDep.Where(x => x.ID == model.ID).First();
			item.StuName = model.StuName;
			item.DepName = model.DepName;
			db.SaveChanges();
			return View();
		}
	}
}
