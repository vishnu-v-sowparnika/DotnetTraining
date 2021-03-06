﻿using EventManagement.Data;
using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EventManagementContext _context;

        public HomeController(EventManagementContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            return View(await _context.EventModels.Include(x=>x.organiser).ToListAsync());

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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventModel = await _context.EventModels.Include(x=>x.organiser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventModel == null)
            {
                return NotFound();
            }

            return View(eventModel);
        }
    }
}
