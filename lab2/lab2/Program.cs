using System;
using System.ComponentModel;

namespace PortSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Port p1 = new Port(0, 100, 200);
            Port p2 = new Port(1, 40, 80);

            Ship s = new Ship(0, p1, 50000, 10, 5, 3, 2, 1.5);
            p1.incomingShip(s);

            Container c1 = new BasicContainer(0, 2000);
            Container c2 = new HeavyContainer(1, 5000);

            p1.containers.Add(c1);
            p1.containers.Add(c2);

            s.load(c1);
            s.load(c2);

            s.reFuel(3000000);

            bool ok = s.sailTo(p2);

            Console.WriteLine("Sailed = " + ok);
            Console.WriteLine("Fuel left: " + s.fuel);
        }
    }
}
