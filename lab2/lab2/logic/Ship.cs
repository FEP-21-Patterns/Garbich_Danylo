using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PortSimulation
{
    public class Ship : IShip
    {
        public int ID;
        public double fuel;

        public Port currentPort;

        public int totalWeightCapacity;
        public int maxNumberOfAllContainers;
        public int maxNumberOfHeavyContainers;
        public int maxNumberOfRefrigeratedContainers;
        public int maxNumberOfLiquidContainers;

        public double fuelConsumptionPerKM;

        private List<Container> containers = new List<Container>();

        public Ship(
            int id,
            Port start,
            int maxWeight,
            int maxAll,
            int maxHeavy,
            int maxRefrigerated,
            int maxLiquid,
            double fuelKm
        )
        {
            ID = id;
            currentPort = start;
            totalWeightCapacity = maxWeight;
            maxNumberOfAllContainers = maxAll;
            maxNumberOfHeavyContainers = maxHeavy;
            maxNumberOfRefrigeratedContainers = maxRefrigerated;
            maxNumberOfLiquidContainers = maxLiquid;
            fuelConsumptionPerKM = fuelKm;
        }

        public List<Container> getCurrentContainers()
        {
            return containers.OrderBy(c => c.ID).ToList();
        }

        public bool load(Container cont)
        {
            if (!currentPort.containers.Contains(cont)) return false;

            if (containers.Count >= maxNumberOfAllContainers) return false;

            int totalW = containers.Sum(c => c.weight) + cont.weight;
            if (totalW > totalWeightCapacity) return false;

            if (cont is HeavyContainer && containers.Count(c => c is HeavyContainer) >= maxNumberOfHeavyContainers)
                return false;

            if (cont is RefrigeratedContainer && containers.Count(c => c is RefrigeratedContainer) >= maxNumberOfRefrigeratedContainers)
                return false;

            if (cont is LiquidContainer && containers.Count(c => c is LiquidContainer) >= maxNumberOfLiquidContainers)
                return false;

            containers.Add(cont);
            currentPort.containers.Remove(cont);
            return true;
        }

        public bool unLoad(Container cont)
        {
            if (!containers.Contains(cont)) return false;

            containers.Remove(cont);
            currentPort.containers.Add(cont);
            return true;
        }

        public void reFuel(double newFuel)
        {
            fuel += newFuel;
        }

        private double containerFuel()
        {
            return containers.Sum(c => c.consumption());
        }

        public bool sailTo(Port p)
        {
            double dist = currentPort.getDistance(p);
            double needed = dist * (fuelConsumptionPerKM + containerFuel());

            if (fuel < needed) return false;

            fuel -= needed;

            currentPort.outgoingShip(this);
            p.incomingShip(this);

            currentPort = p;

            return true;
        }
    }
}
