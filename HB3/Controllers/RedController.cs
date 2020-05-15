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
    public class RedController:Controller
    {
        HBContext db;

        public RedController(HBContext user)
        {
            db = user;
        }
    }
}
