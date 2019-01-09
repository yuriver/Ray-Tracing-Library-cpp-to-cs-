using System.IO;

namespace Ray_Tracing_Library_cpp_to_cs_
{
    class PPMExample
    {
        public PPMExample()
        {
            int nx = 200;
            int ny = 100;

            using (StreamWriter sw = new StreamWriter("PPM Example.ppm"))
            {
                sw.WriteLine("P3");
                sw.WriteLine(string.Format("{0} {1}", nx, ny));
                sw.WriteLine("255");


                for (int i = 0; i <= ny; i++) // 0 to 100
                {
                    for (int j = 0; j < nx; j++) // 0 to 200
                    {
                        // normalize
                        float r = j / (float)nx;
                        float g = i / (float)ny;
                        float b = 0f;

                        // calculate each rgb value
                        int ir = (int)(255.99f * r);
                        int ig = (int)(255.99f * g);
                        int ib = (int)(255.99f * b);

                        sw.WriteLine(string.Format("{0} {1} {2}", ir, ig, ib));
                    }
                }
            }
        }
    }
}
