using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {

    [HttpGet("/animals")]
    public ActionResult Index()
    {
      List<Animal> animalDB = Animal.GetAll();
      return View(animalDB);
    }

    [HttpGet("/animals/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/animals")]
    public ActionResult Create(string type, string name, string desc)
    {
      Animal myAnimalObj = new Animal(type, name, desc);
      myAnimalObj.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/animals/{id}")]
    public ActionResult Show(int id)
    {
      Animal foundAnimal = Animal.Find(id);
      return View(foundAnimal);
    }
    [HttpGet("/animals/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Animal editAnimal = Animal.Find(id);
      return View(editAnimal);
    }

    [ActionName("Edit"), HttpPost("/animals/edit/{id}")]
    public ActionResult Update(int id, string type, string name, string desc)
    {
      Animal thisAnimal = Animal.Find(id);
      thisAnimal.Edit(type, name, desc);
      return RedirectToAction("Show", new{id = id});
    }



    [ActionName("Destroy"), HttpPost("/animals/delete/{id}")]
    public ActionResult Destroy(int id)
    {
      Animal.Delete(id);
      return RedirectToAction("Index");
    }
  }
}
