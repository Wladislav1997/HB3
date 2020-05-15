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
    public class AddController: Controller
    {
        HBContext db;

        public AddController(HBContext user)
        {
            db = user;
        }
        [HttpGet]
        public IActionResult AddOperation(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Plan");
            }
            else
            {
                AddOp addOp = new AddOp();
                addOp.id = id;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOperation(AddOp addOp)
        {
            db.Operations.Add(new Operation { Name = addOp.Name, NameAct = addOp.NameAct, Sum = addOp.Sum, Coment = addOp.Coment, PlanId = addOp.PlId });
            await db.SaveChangesAsync();
            return RedirectToAction("Operation");
        }
        //[HttpGet]
        //public IActionResult AddPlan()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> AddPlan(int? id)
        //{
        //    return View();
        //}
        //public async Task<IActionResult> AddOperationAct()
        //{
        //    return View();
        //}
    }
}
