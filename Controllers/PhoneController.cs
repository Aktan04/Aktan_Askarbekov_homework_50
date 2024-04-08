using Hw50.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hw50.Controllers;

public class PhoneController : Controller
{
    private readonly IWebHostEnvironment _appEnvironment;
    private MobileContext _context;

    public PhoneController(MobileContext context, IWebHostEnvironment appEnvironment)
    {
        _context = context;
        _appEnvironment = appEnvironment;
    }
    
    public IActionResult Index()
    {
        var phones = _context.Phones.ToList();
        return View(phones);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Phone phone)
    {
        if (phone != null)
        {
            _context.Add(phone);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}