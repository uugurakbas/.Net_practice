using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IBB.Models;
using IBB.Services;
using System.Threading.Tasks;

namespace IBB.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IIsparkService _isparkService;

    public HomeController(ILogger<HomeController> logger, IIsparkService isparkService)
    {
        _logger = logger;
        _isparkService = isparkService;
    }

    public async Task<IActionResult> Index()
    {
        var isparkData = await _isparkService.GetIsparkDataAsync();
        return View(isparkData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
