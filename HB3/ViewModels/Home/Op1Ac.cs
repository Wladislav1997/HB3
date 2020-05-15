﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB3.Models;

namespace HB3.ViewModels.Home
{
    public class Op1Ac
    {
        public IQueryable<P1> p1s { get; set; }
        public string Name { get; set; }
        public DateTime StData { get; set; }
        public DateTime FinData { get; set; }
        public string NameAct { get; set; }
        public int maxsum { get; set; }
        public int minsum { get; set; }
        public int id { get; set; }
    }
}
