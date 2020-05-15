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
        public async Task<IActionResult> Operation(Op op, string st, int id)
        {
            IQueryable<Operation> operat = db.Operations.Include(p => p.Plan).Include(c => c.p);
            operat = operat.Where(p => p.Plan.User.Email == User.Identity.Name);
            if (id != 0)
            {
                operat = operat.Where(p => p.PlanId == id);
                Op op1 = new Op();
                op1.id = id;
                op1.Operations = operat;
                return View(op1);
            }
            else
            {
                if (st == "выполняются")
                {
                    operat = operat.Where(p => p.Plan.Data <= DateTime.Now && p.Plan.DataPeriod >= DateTime.Now);
                    foreach (Operation op1 in operat)
                    {
                        int pr = op1.Sum / 100;// 1%
                        int s = 0;
                        foreach (P p in op1.p)
                        {
                            s += p.Sum;
                        }
                        op1.SumP = s;
                        op1.Procent = s / pr;
                        db.Operations.Update(op1);

                    }
                    await db.SaveChangesAsync();
                    Op op2 = new Op();
                    op2.Operations = operat;
                    return View(op2);
                }
                else if (st == "ожидают")
                {
                    operat = operat.Where(p => p.Plan.Data > DateTime.Now);
                    Op op2 = new Op();
                    op2.Operations = operat;
                    return View(op2);
                }
                else if (st == "выполнены")
                {
                    operat = operat.Where(p => p.Plan.DataPeriod < DateTime.Now);
                    Op op2 = new Op();
                    op2.Operations = operat;
                    return View(op2);
                }
                else
                {
                    Op op2 = new Op();
                    op2.Operations = operat;
                    return View(op2);
                }
            }


        }
        public async Task<IActionResult> Plan(Pl plan, int id, string st)
        {
            IQueryable<Plan> pl = db.Plans.Include(c => c.User).Include(u => u.Operations).ThenInclude(u => u.p);
            pl = pl.Where(p => p.User.Email == User.Identity.Name);
            if (id != 0)
            {
                pl = pl.Where(p => p.Id == id);
                Pl pl2 = new Pl();
                pl2.pl = pl;
                return View(pl2);
            }
            else
            {
                if (st == "выполняются")
                {
                    pl = pl.Where(p => p.Data <= DateTime.Now && p.DataPeriod >= DateTime.Now);
                    foreach (Plan p in pl)
                    {
                        int i = 0;
                        int t = 0;
                        int ras = 0;
                        int doch = 0;
                        foreach (Operation op in p.Operations)
                        {
                            if (op.NameAct == "доход")
                            {
                                doch += op.SumP;
                            }
                            else
                            {
                                ras += op.SumP;
                            }
                            i += op.Procent;
                            t++;
                        }
                        p.RasMonth = ras;
                        p.DochMonth = doch;
                        p.RaznDochRas = doch - ras;
                        if (i != 0)
                        {
                            p.Procent = i / t;
                        }
                        else
                        {
                            p.Procent = 0;
                        }
                        db.Plans.Update(p);
                    }
                    await db.SaveChangesAsync();
                    return View(pl);
                }
                else if (st == "ожидают")
                {
                    pl = pl.Where(p => p.Data > DateTime.Now);
                    Pl pl1 = new Pl();
                    pl1.pl = pl;
                    return View(pl1);
                }
                else if (st == "выполнены")
                {
                    pl = pl.Where(p => p.DataPeriod < DateTime.Now);
                    Pl pl1 = new Pl();
                    pl1.pl = pl;
                    return View(pl1);
                }
                else
                {
                    Pl pl1 = new Pl();
                    pl1.pl = pl;
                    return View(pl1);
                }
            }

        }
        public IActionResult P(OpAc opAc, int? id)
        {
            IQueryable<P> p = db.Ps.Include(c => c.Operation);
            p = p.Where(p => p.Operation.Plan.User.Email == User.Identity.Name);
            if (id != null)
            {
                p = p.Where(p => p.OperationId == id);
                OpAc opAc1 = new OpAc();
                opAc1.ps = p;
                return View(opAc1);
            }
            else
            {
                OpAc opAc1 = new OpAc();
                opAc1.ps = p;
                return View(opAc1);
            }
        }
       

    }
}
