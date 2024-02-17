# RayTracer
This repository contains a custom implementation of a basic ray tracing algorithm designed to generate photorealistic images by simulating the way light interacts with objects. Specifically, this implementation focuses on rendering a scene featuring spheres of various colors and sizes viewed from different angles, showcasing the algorithm's ability to handle reflections, shadows, and the intricate nuances of light interaction. The output images would look something like this:

![071](https://github.com/Alexandra2802/RayTracer/assets/76787341/59488df9-29b0-4c1e-9322-96e2d02fd4a1)

## About Ray Tracing
Ray tracing is a rendering technique that computes the color of pixels by tracing the path that light would take as it travels through a scene. The basic idea is simple yet powerful: for each pixel in the image, a ray is cast from the camera (viewer’s eye position) through that pixel. The ray extends into the scene, intersecting with any objects it encounters. When a ray intersects an object, we calculate the color of that point on the surface. The algorithm considers various optical effects, including reflection, refraction, and shadows.

![image](https://github.com/Alexandra2802/RayTracer/assets/76787341/fadbe208-2b4e-421a-bd30-47acdf953598)


This technique can simulate complex optical effects, including:

- Direct illumination: Light coming directly from a light source.
- Shadows: Areas that receive no direct light because of obstruction by other objects.
- Reflections and Refractions: Light bouncing off surfaces or passing through transparent materials, respectively.
- Global Illumination: The effect of light bouncing multiple times within a scene, contributing to the lighting of each point.

