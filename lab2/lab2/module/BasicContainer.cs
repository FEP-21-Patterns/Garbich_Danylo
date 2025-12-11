namespace PortSimulation
{
    public class BasicContainer : Container
    {
        public BasicContainer(int id, int w)
            : base(id, w)
        {
        }

        public override double consumption()
        {
            return 2.50 * weight;
        }
    }
}
