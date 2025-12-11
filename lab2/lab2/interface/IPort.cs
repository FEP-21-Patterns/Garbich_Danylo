using System.Collections.Generic;

namespace PortSimulation
{
    public interface IPort
    {
        void incomingShip(Ship s);
        void outgoingShip(Ship s);
    }
}
