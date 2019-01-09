using System;
using System.IO;
using UnityEngine;

namespace Ray_Tracing_Library_cpp_to_cs_
{
    class RayExample
    {
        public RayExample()
        {
            using (StreamWriter sw = new StreamWriter("RayExample.ppm"))
            {
                int nx = 200;
                int ny = 100;

                sw.WriteLine(string.Format("P3\n{0} {1}\n255", nx, ny));

                Vector3 leftBottom = new Vector3(-2f, -1f, -1f);
                Vector3 horizontal = new Vector3(4f, 0f, 0f);
                Vector3 vertical = new Vector3(0f, 2f, 0f);
                Vector3 origin = Vector3.zero;

                for(int i=0; i<ny; i++)
                {
                    for(int j=0; j<nx; j++)
                    {
                        float u = j / (float)nx;
                        float v = i / (float)ny;
                        Ray ray = new Ray(origin, leftBottom + u * horizontal + v * vertical);
                        Vector3 color = GetColor(ref ray);

                        int ir = (int)(255.99f * color[0]);
                        int ig = (int)(255.99f * color[1]);
                        int ib = (int)(255.99f * color[2]);

                        sw.WriteLine(string.Format("{0} {1} {2}", ir, ig, ib));
                    }
                }
            }                
        }

        Vector3 GetColor(ref Ray ray)
        {
            Vector3 eyePoint = new Vector3(.5f, .7f, 1f);
            Vector3 normalDir = ray.direction.normalized;
            float t = .5f * (normalDir.y + 1f);

            return (1f - t) * Vector3.one + t * eyePoint;
        }
    }
}
