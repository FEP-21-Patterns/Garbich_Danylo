using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PortSimulation
{
    public class Port : IPort
    {
        public int ID;
        public double latitude;
        public double longitude;

        public List<Container> containers = new List<Container>();
        public List<Ship> history = new List<Ship>();
        public List<Ship> current = new List<Ship>();

        public Port(int id, double lat, double lon)
        {
            ID = id;
            latitude = lat;
            longitude = lon;
        }

        public void incomingShip(Ship s)
        {
            if (!current.Contains(s))
                current.Add(s);

            if (!history.Contains(s))
                history.Add(s);
        }

        public void outgoingShip(Ship s)
        {
            current.Remove(s);
            if (!history.Contains(s))
                history.Add(s);
        }

        public double getDistance(Port other)
        {
            return Math.Sqrt(
                Math.Pow(latitude - other.latitude, 2) +
                Math.Pow(longitude - other.longitude, 2)
            );
        }
    }
}
