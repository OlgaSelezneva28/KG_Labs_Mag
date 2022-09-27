using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG_2_9
{
    public class Point
    {
        public float x, y, z;

        public Point()
        {
        }

        public Point(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public Point(Double X, Double Y, Double Z)
        {
            x = (float)X;
            y = (float)Y;
            z = (float)Z;
        }

        public Point(Double X, Double Y)
        {
            x = (float)X;
            y = (float)Y;
        }
    }
}
