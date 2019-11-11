using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop_1_Algoritmer
{
    /// <summary>
    /// The class for encapsulating the location data of a specific car
    /// </summary>
    public class LocationData
    {
        public int id;
        public double x, y;

        //The class constructor
        public LocationData(int id, double x, double y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }
    }
}
