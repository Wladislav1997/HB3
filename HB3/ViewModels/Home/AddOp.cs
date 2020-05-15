using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB3.Models;

namespace HB3.ViewModels.Home
{
    public class AddOp
    {
        public string Name { get; set; }
        public string NameAct { get; set; } // доход расход
        public string Coment { get; set; }
        public int Sum { get; set; }
        public int id { get; set; }
        public int PlId { get; set; }
    }
}
