# Ch01 Output an image

렌더러를 시작할 때마다 이미지를 볼 수 있는 방법이 필요하다.  
가장 직접적인 방법은 파일에 기록하는 것이다.  
매우 다양한 형식이 있으며, 대부분 복잡한 형식을 지닌다.  
그중 간단한 PPM을 소개하겠다.  

## PPM Format

![PPM example](https://upload.wikimedia.org/wikipedia/commons/5/57/Tiny6pixel.png)
```
P3  
# The P3 means colors are in ASCII, then 3 columns and 2 rows,  
# then 255 for max color, then RGB triplets  
3 2  
255  
255    0  0    0  255    0   0   0  255  
255  255  0  255  255  255   0   0    0  
```

## PPM Example
```csharp
void PPMEaxmple()
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
```

* 픽셀은 행방향은 위에서 아래로, 열방향은 왼쪽에서 오른쪽으로 쓰여진다.
* Left Top      : Black(0, 0, 0)
* Right Top     : Red(255, 0, 0)
* Left Bottom   : Green(0, 255, 0) 
* Right Bottom  : Yellow(255, 255, 0)
* 위와 같은 코드를 실행하여, PPM Viewer로 파일을 열어보면 다음과 같은 이미지를 확인할 수 있다.
![PPM example](https://user-images.githubusercontent.com/15705675/50724898-23531900-1138-11e9-91fa-5abf198bd811.png)
