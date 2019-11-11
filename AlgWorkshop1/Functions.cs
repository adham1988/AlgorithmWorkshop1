using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop_1_Algoritmer
{
    public static class Functions
    {
        public static double Hash(double val, double min, double max, int interval)
        {
            var Scale = (int)(val + Math.Abs(min)) / (max - min);

            var Index = Scale * interval;

            return Index;
        }

        public static double[] FindMinMax(List<LocationData> data)
        {
            double xMin = data[0].x;
            double xMax = data[0].x;
            double yMin = data[0].y;
            double yMax = data[0].y;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].x < xMin)
                {
                    xMin = data[i].x;
                }
                if (data[i].x > xMax)
                {
                    xMax = data[i].x;
                }

                if (data[i].y < yMin)
                {
                    yMin = data[i].y;
                }
                if (data[i].y > yMax)
                {
                    yMax = data[i].y;
                }
            }
            double[] values = new double[] { xMin, xMax, yMin, yMax };
            return values;
        }
    }
}
