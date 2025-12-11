namespace PortSimulation
{
    public class HeavyContainer : Container
    {
        public HeavyContainer(int id, int w)
            : base(id, w)
        {
        }

        public override double consumption()
        {
            return 3.00 * weight;
        }
    }
}
