﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASPProjekt.Data;
using ASPProjekt.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASPProjekt.Pages
{
    [Authorize]
    public class EventsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EventsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.ToListAsync();
        }
    }
}
