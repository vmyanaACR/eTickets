using eTickets.Data;
using eTickets.Data.Services;
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
        var data = await _actorService.GetAll();
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }
}