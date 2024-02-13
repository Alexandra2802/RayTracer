using System;

namespace rt
{
    public class Sphere : Geometry
    {
        private Vector Center { get; set; }
        private double Radius { get; set; }

        public Sphere(Vector center, double radius, Material material, Color color) : base(material, color)
        {
            Center = center;
            Radius = radius;
        }

        public override Intersection GetIntersection(Line line, double minDist, double maxDist)
        {
            // ADD CODE HERE: Calculate the intersection between the given line and this sphere
            bool valid;
            bool visible;
            double t;

            var a = line.Dx.X;
            var c = line.Dx.Y;
            var e = line.Dx.Z;

            var b = line.X0.X;
            var d = line.X0.Y;
            var f = line.X0.Z;

            var A = a * a + c * c + e * e;
            var B = 2 * (a * b - a * Center.X + c * d - c * Center.Y + e * f - e * Center.Z);
            var C = b * b + Center.X * Center.X - 2 * b * Center.X + d * d + Center.Y * Center.Y - 2 * d * Center.Y + f * f + Center.Z * Center.Z - 2 * f * Center.Z - Radius * Radius;
            //A*t^2+B*t+C=0
            var delta = B * B - 4 * A * C;
            valid = (delta >= 0);
            if (valid)
            {
                var t1 = (-B + Math.Sqrt(delta)) / 2 * A;
                var t2 = (-B - Math.Sqrt(delta)) / 2 * A;
                t = t1 < t2 ? t1 : t2;
            }
            else
                t = -1;
            visible=(minDist<t && t<maxDist);

            return new Intersection(valid, visible, this, line, t);
        }

        public override Vector Normal(Vector v)
        {
            var n = v - Center;
            n.Normalize();
            return n;
        }
    }
}