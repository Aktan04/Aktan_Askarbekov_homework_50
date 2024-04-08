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
    
    public IActionResult DownloadFile(string phoneName)
    {
        if (string.IsNullOrWhiteSpace(phoneName))
        {
            return NotFound();
        }

        string sanitizedCompanyName = phoneName.ToLower();
        string directoryPath = Path.Combine(_appEnvironment.WebRootPath, "txtFiles");
        string[] files = Directory.GetFiles(directoryPath);
        string matchingFile = files.FirstOrDefault(file => Path.GetFileNameWithoutExtension(file) == sanitizedCompanyName);

        if (!string.IsNullOrEmpty(matchingFile))
        {
            return PhysicalFile(matchingFile, "application/octet-stream", Path.GetFileName(matchingFile));
        }
        else
        {
            return NotFound();
        }
    }
    
    public IActionResult RedirectToSite(string company)
    {
        string redirectUrl = "https://" + company + ".com";
        return RedirectPermanent(redirectUrl);
    }
    
    public IActionResult Edit(int id)
    {
        var phone = _context.Phones.Find(id);
        if (phone == null)
        {
            return NotFound();
        }
        return View(phone);
    }

    [HttpPost]
    public IActionResult Edit(Phone phone)
    {
        if (ModelState.IsValid)
        {
            _context.Update(phone);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(phone);
    }
}