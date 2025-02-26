using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController: Controller {

        public IActionResult Index(){


            int saat =  DateTime.Now.Hour;
            int UserCount = Repository.Users.Where(i=>i.WillAttend == true).Count();
            ViewData["Selamlama"] = saat > 12 ? "İyi Günler":"Günaydın";


            var MeetingInfo = new MeetingInfo(){
                
                Id= 1,
                Location = "İstanbul, Abc kongre merkezi",
                Date = new DateTime(2024, 01, 20, 20, 0, 0),
                NumberOfPeople = UserCount
            };
            
            return  View(MeetingInfo);
        }
    }
}