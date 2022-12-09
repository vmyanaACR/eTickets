using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class ActorsController : Controller
{
    private readonly IActorService _actorService;

    public ActorsController(IActorService actorService)
    {
        _actorService = actorService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _actorService.GetAllAsync();
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
    {
        if (! ModelState.IsValid)
        {
            return View(actor);
        }

        await _actorService.AddAsync(actor);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var actor = await _actorService.GetByIdAsync(id);
        if (actor == null) return View("Empty");
        return View(actor);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var actor = await _actorService.GetByIdAsync(id);
        if (actor == null) return View("NotFound");
        return View(actor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")]Actor actor)
    {
        if (! ModelState.IsValid)
        {
            return View(actor);
        }

        await _actorService.UpdateAsync(id, actor);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var actorDetails = await _actorService.GetByIdAsync(id);
        if (actorDetails == null) return View("NotFound");
        return View(actorDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var actorDetails = await _actorService.GetByIdAsync(id);
        if (actorDetails == null) return View("NotFound");

        await _actorService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}