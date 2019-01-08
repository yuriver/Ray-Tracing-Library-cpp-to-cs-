using System;
using System.IO;

using UnityEngine;

namespace Ray_Tracing_Library_cpp_to_cs_
{
    class VectorExample
    {
        public VectorExample()
        {
            using (StreamWriter sw = new StreamWriter("VectorExample.ppm"))
            {
                int nx = 200;
                int ny = 100;

                sw.WriteLine(string.Format("P3\n{0} {1}\n255", nx, ny));
                for (int i = 0; i < ny; i++)
                {
                    for (int j = 0; j < nx; j++)
                    {
                        Vector3 color = new Vector3(j / (float)nx, i / (float)ny, 0f);
                        int ir = (int)(255.99f * color[0]);
                        int ig = (int)(255.99f * color[1]);
                        int ib = (int)(255.99f * color[2]);

                        sw.WriteLine(string.Format("{0} {1} {2}", ir, ig, ib));
                    }
                }
            }
        }
    }
}
