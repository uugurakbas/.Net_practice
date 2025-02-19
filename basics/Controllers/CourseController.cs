using basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace  basics.Controllers;

public class CourseController: Controller {

    public IActionResult Index()
    {
        var kurs = new Course();

        kurs.Id = 1;
        kurs.Title = "Asp kursu";
        kurs.Description ="asfdgdfg";
        kurs.Image = "b.jpg";

        return View(kurs);
    }
    public IActionResult Details(int? id)
    {
        if(id == null){
            return Redirect("/course/list"); 
        }
        var kurs = Repository.GetById(id);

        return View(kurs);
    }

    public IActionResult List()
    {

        return View("CourseList", Repository.Courses);
    }
}