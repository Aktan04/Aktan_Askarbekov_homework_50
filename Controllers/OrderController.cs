using Hw50.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hw50.Controllers;

public class OrderController : Controller
{
    private readonly MobileContext _db;
    
    public OrderController(MobileContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        List<Order> orders = _db.Orders.Include(o => o.Phone).ToList();
        return View(orders);
    }
    
    public IActionResult Create(int phoneId)
    {
        Phone phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
        return View(new Order() {Phone = phone});
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        if (order != null)
        {
            _db.Add(order);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}