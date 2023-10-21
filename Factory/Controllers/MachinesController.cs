using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Factory.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "Machines";
      return View(_db.Machines.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add Machine";
      return View();
    
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
       if (!ModelState.IsValid)
    {
        ModelState.AddModelError("", "Please correct the errors and try again.");
        return View(machine);
    }
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      ViewBag.PageTitle = "Machine Details";
      Machine thisMachine = _db.Machines
        .Include(machine => machine.JoinEntities)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.PageTitle = "Edit Machine";
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      if (!ModelState.IsValid)
    {
        ModelState.AddModelError("", "Please correct the errors and try again.");
        return View(machine);
    }
      _db.Machines.Update(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      ViewBag.PageTitle = "Delete Machine";
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}