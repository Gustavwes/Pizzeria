﻿using System;
using System.Collections.Generic;

namespace Pizzeria.Models
{
    public partial class BestallningMatratt
    {
        public int MatrattId { get; set; }
        public int BestallningId { get; set; }
        public int Antal { get; set; }

        public Bestallning Bestallning { get; set; }
        public Matratt Matratt { get; set; }
    }
}
