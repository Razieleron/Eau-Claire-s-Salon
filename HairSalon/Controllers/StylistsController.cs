using HairSalon.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HairSalon.Controllers;

public class StylistsController : Controller
{
  private readonly HairSalonContext _db;

  public StylistsController(HairSalonContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Stylist> model = _db.Stylists.ToList();
    return View(model);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Stylist newStylist)
  {
    _db.Stylists.Add(newStylist);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  
  public ActionResult Details(int id)
  {
    Stylist targetStylist = _db.Stylists.Include(stylist => stylist.Clients).FirstOrDefault(stylist => stylist.StylistId == id);
    return View(targetStylist);
  }
  public ActionResult Delete(int id)
  {
    Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
    return View(thisStylist);
  }
  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
    _db.Stylists.Remove(thisStylist);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
}