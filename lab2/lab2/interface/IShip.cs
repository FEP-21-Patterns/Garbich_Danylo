using System.ComponentModel;

namespace PortSimulation
{
    public interface IShip
    {
        bool sailTo(Port p);
        void reFuel(double newFuel);
        bool load(Container cont);
        bool unLoad(Container cont);
    }
}
