using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class Car
    {
        private Random rand = new Random();
        public Vector2 Position { get; set; }

        float speed;
        public float Speed { get { return speed; } set { speed = Math.Min(value, MaxSpeed); } }

        public float MaxSpeed { get; set; }

        public float TargetSpeed { get; set; }

        public Direction Direction { get; set; }

        public float Acceleration { get; set; }

        public float Deceleration { get; set; }

        public float BrakingDistance { get; set; }


        public Car(Vector2 position, float acceleration, float deceleration, Direction dir, float maxSpeed, float targetSpeed = 0f, float brakingDist = 500f)
        {
            Position = position;
            Acceleration = acceleration;
            Deceleration = deceleration;
            Direction = dir;
            MaxSpeed = maxSpeed;
            TargetSpeed = targetSpeed;
            BrakingDistance = brakingDist;
        }


        public void Update()
        {




            if(TargetSpeed > speed)
            {
                Speed += Acceleration * (TargetSpeed-Speed);
                Speed = Math.Min(speed, TargetSpeed);
            }
            else if(TargetSpeed < speed)
            {
                Speed -= Deceleration * (Speed - TargetSpeed);
                Speed = Math.Max(speed, TargetSpeed);
            }
            if((TargetSpeed - speed)*(TargetSpeed-speed) < 4)
            {
                Speed = TargetSpeed;
            }








            Position = new Vector2(Position.X + (Direction == Direction.East ? speed : (Direction == Direction.West ? -speed : 0)), Position.Y + (Direction == Direction.North ? -speed : (Direction == Direction.South ? speed : 0)));
        }


    }
}
