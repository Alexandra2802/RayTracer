using System;

namespace rt
{
    class RayTracer
    {
        private Geometry[] geometries;
        private Light[] lights;

        public RayTracer(Geometry[] geometries, Light[] lights)
        {
            this.geometries = geometries;
            this.lights = lights;
        }

        private double ImageToViewPlane(int n, int imgSize, double viewPlaneSize)
        {
            var u = n * viewPlaneSize / imgSize;
            u -= viewPlaneSize / 2;
            return u;
        }

        private Intersection FindFirstIntersection(Line ray, double minDist, double maxDist)
        {
            var intersection = new Intersection();

            foreach (var geometry in geometries)
            {
                var intr = geometry.GetIntersection(ray, minDist, maxDist);

                if (!intr.Valid || !intr.Visible) continue;

                if (!intersection.Valid || !intersection.Visible)
                {
                    intersection = intr;
                }
                else if (intr.T < intersection.T)
                {
                    intersection = intr;
                }
            }

            return intersection;
        }

        private bool IsLit(Vector point, Light light)
        {
            // ADD CODE HERE: Detect whether the given point has a clear line of sight to the given light
            Vector pointToLight = light.Position - point;
            Intersection intersection = FindFirstIntersection(new Line(point, light.Position), 0, pointToLight.Length());
            return !intersection.Visible || !intersection.Valid;
        }

        public void Render(Camera camera, int width, int height, string filename)
        {
            var background = new Color();
            var image = new Image(width, height);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    // ADD CODE HERE: Implement pixel color calculation
                    var x1 = camera.Position +
                            camera.Direction * camera.ViewPlaneDistance +
                            camera.Up * ImageToViewPlane(j, height, camera.ViewPlaneHeight) +
                            (camera.Up^camera.Direction) * ImageToViewPlane(i, width, camera.ViewPlaneWidth);
                    Line ray = new Line(camera.Position, x1);
                    Intersection intersection = FindFirstIntersection(ray, camera.FrontPlaneDistance, camera.BackPlaneDistance);
                    Geometry geometry = intersection.Geometry;

                    if (intersection.Valid)
                    {
                        Vector N = intersection.Geometry.Normal(intersection.Position);
                        Vector E = (camera.Position - intersection.Position).Normalize();
                        Color color = geometry.Material.Ambient;

                        foreach (Light light in lights)
                        {
                            color *= light.Ambient;
                            if (IsLit(intersection.Position, light))
                            {
                                Vector T = (light.Position - intersection.Position).Normalize();
                                Vector R = (N * (N * T) * 2 - T).Normalize();
                                if (N * T > 0)
                                {
                                    color += geometry.Material.Diffuse * light.Diffuse * (N * T);
                                }
                                if (E * R > 0)
                                {
                                    color += geometry.Material.Specular * light.Specular * Math.Pow(E * R, geometry.Material.Shininess);
                                }
                                color *= light.Intensity;
                            }
                        }
                        image.SetPixel(i, j, color);
                    }
                }
            }
            image.Store(filename);
        }
    }
}