using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class Intersection
    {
        public IntersectionDirection Direction { get; set; }

        public Vector2 Position { get; set; }

        public Street[] Streets { get; set; }

        public Dictionary<Street, Car> LastCarsToPass { get; private set; }

        public TimeSpan WaitTime { get; set; }

        private TimeSpan timePassed;

        public Intersection(TimeSpan waitTime, Vector2 position, IntersectionDirection direction, Street[] streets)
        {
            WaitTime = waitTime;
            Position = position;
            Direction = direction;
            timePassed = new TimeSpan(0);
            Streets = streets;
            LastCarsToPass = new Dictionary<Street, Car>();
            foreach(Street s in Streets)
            {
                LastCarsToPass.Add(s, null);
            }
        }

        public void Update(TimeSpan elapsedTime)
        {
            timePassed = timePassed.Add(elapsedTime);
            if(timePassed.Seconds >= WaitTime.Seconds)
            {
                timePassed = new TimeSpan(0);

                Direction = (IntersectionDirection)((int)Direction * -1 + 1);
            }
            foreach(Street s in Streets)
            {
                foreach(Car c in s.Cars)
                {
                    if(s.Direction == TrafficSim.Direction.North || s.Direction == TrafficSim.Direction.South)
                    {
                        if(Math.Abs(c.Position.Y - Position.Y) <= c.Speed)
                        {
                            LastCarsToPass[s] = c;
                        }
                    }
                    else
                    {
                        if (Math.Abs(c.Position.X - Position.X) <= c.Speed)
                        {
                            LastCarsToPass[s] = c;
                        }
                    }
                }
            }
        }
    }
}
