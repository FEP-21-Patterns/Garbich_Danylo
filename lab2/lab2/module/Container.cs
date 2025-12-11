namespace PortSimulation
{
    public abstract class Container
    {
        public int ID;
        public int weight;

        public Container(int id, int w)
        {
            ID = id;
            weight = w;
        }

        public abstract double consumption();

        public bool equals(Container other)
        {
            return other != null &&
                ID == other.ID &&
                weight == other.weight &&
                this.GetType() == other.GetType();
        }
    }
}
