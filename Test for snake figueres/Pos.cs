﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_for_snake_figueres
{
    public readonly struct Pos
    {
        public int X { get; }
        public int Y { get; }

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
