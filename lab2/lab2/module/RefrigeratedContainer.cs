namespace PortSimulation
{
    public class RefrigeratedContainer : HeavyContainer
    {
        public RefrigeratedContainer(int id, int w)
            : base(id, w)
        {
        }

        public override double consumption()
        {
            return 5.00 * weight;
        }
    }
}
