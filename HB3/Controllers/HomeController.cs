using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HB3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HB3.ViewModels.Home;

namespace HB3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        HBContext db;

        public HomeController(HBContext user)
        {
            db = user;
        }
        public async Task<IActionResult> Index(HB3.ViewModels.Home.Index index)
        {
            var operat = await db.Operations.Include(c => c.p)
                .Include(u => u.Plan)
                .Where(p => p.Plan.User.Email == User.Identity.Name && p.Plan.Data <= DateTime.Now && p.Plan.DataPeriod >= DateTime.Now)
                .ToListAsync();
            foreach (Operation op in operat)
            {
                int pr = op.Sum / 100;// 1%
                int s = 0;
                foreach (P p in op.p)
                {
                    s += p.Sum;
                }
                op.SumP = s;
                op.Procent = s / pr;
                db.Operations.Update(op);

            }
            await db.SaveChangesAsync();
            HB3.ViewModels.Home.Index ind = new HB3.ViewModels.Home.Index();

            ind.Operations = operat;
            return View(ind);
        }
    }
}
