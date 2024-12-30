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

    public IActionResult List()
    {
        var kurslar = new List<Course>(){
            new Course() {Id = 1, Title = "1. kurs", Description = "bla bla bla",Image="b.jpg"},
            new Course() {Id = 2, Title = "2. kurs", Description = "zla bla bla",Image="c.jpg"},
            new Course() {Id = 3, Title = "3. kurs", Description = "fla bla bla",Image="b.jpg"},
            new Course() {Id = 4, Title = "4. kurs", Description = "gla bla bla",Image="c.jpg"},
        };
        return View("CourseList", kurslar);
    }
}