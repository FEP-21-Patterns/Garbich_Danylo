namespace PortSimulation
{
    public class LiquidContainer : HeavyContainer
    {
        public LiquidContainer(int id, int w)
            : base(id, w)
        {
        }

        public override double consumption()
        {
            return 4.00 * weight;
        }
    }
}
