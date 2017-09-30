using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class Street
    {
        public Direction Direction { get; set; }

        public float Pos { get; set; }

        public LinkedList<Car> Cars { get; set; }

        public Street(Direction direction, float distAcross)
        {
            Direction = direction;
            Pos = distAcross;
            Cars = new LinkedList<Car>();
        }

        public Street(Direction direction, float distAcross, LinkedList<Car> cars)
        {
            Cars = cars;
        }

    }
}
