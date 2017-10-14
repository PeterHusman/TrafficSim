using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class Line
    {
        private Rectangle line;

        public Color Color;

        public float Rotation;

        public Line(int x1, int y1, int x2, int y2, Color color, int width)
        {
            Vector2 startPoint = new Vector2(x1, y1);
            Vector2 endPoint = new Vector2(x2, y2);
            Vector2 distanceVector = endPoint - startPoint;

            Rotation = distanceVector.ToAngle();

            line = new Rectangle(x1,y1,distanceVector.Length().ToInt(), width);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.Pixel, line, null, Color, Rotation, Vector2.Zero, SpriteEffects.None, 1);
        }

    }
    public static class Extensions
    {
        public static int ToInt(this float value) => (int)value;

        public static float ToFloat(this double value) => (float)value;

        public static float ToAngle(this Vector2 value) => Math.Atan2(value.Y, value.X).ToFloat();
    }
}
