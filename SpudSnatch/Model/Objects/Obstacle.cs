﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpudSnatch.Model.Objects
{
    public class Obstacle
    {
        int positionX;
        int positionY;
        public int[] GetLocation()
        {
            int[] position = new int[2];
            position[1] = positionX;
            position[2] = positionY;
            return position;
        }
    }
}
