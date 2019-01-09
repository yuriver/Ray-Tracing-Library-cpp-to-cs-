# Ch03 Rays, a simple camera, and background
  
모든 ray tracer에 있는 한 가지는 ray 클래스이며, ray를 따라 보이는 색상의 계산이다.  
ray를 함수 p(t) = A + t * B로 생각해 보자.  
여기서 p는 3D의 선상에 있는 3D 위치이다.  
A는 ray의 원점이고 B는 ray의 방향이다.  
ray 매개 변수 t는 실수이다.  
다른 t를 연결하면 p(t)가 ray를 따라 점을 이동한다.
- 음수 t를 연결하면 3D 행의 아무 곳이나 이동할 수 있다.  
- 양수 t의 경우 A의 앞 부분만 가져오고 이것은 half-line 또는 ray라고도 한다.  
    
## C = p(2)

![ray formula example](https://user-images.githubusercontent.com/15705675/50883450-72949480-142b-11e9-8513-5b802dda0d1f.png)


## Import Ray Class

Ray 클래스는 구현하지 않고, UnityEngine의 Ray 클래스를 가져오도록 한다.
![Ray Import](https://user-images.githubusercontent.com/15705675/50839977-d7f17280-13a4-11e9-8f4d-f7c230ba0723.PNG)

코너를 돌면서 ray tracer를 만들 준비가 되었다.  
ray tracer의 핵심은 ray를 픽셀을 통해 보내고 ray가 그 광선의 방향으로 어떤 색을 띄는지 계산하는 것이다.  
이것은 ray가 눈에서 픽셀로 가는 ray를 계산하고, ray가 교차하는 것을 계산하고, 교차점의 색상을 계산한다.  
ray tracer를 처음 개발할 때는 항상 코드를 작성하고 실행하기 위한 간단한 카메라를 사용한다.  
배경의 색을 반환하는 간단한 색상(ray) 함수를 만든다(간단한 그라디언트).  
  
![Ray Explain](https://user-images.githubusercontent.com/15705675/50885118-226c0100-1430-11e9-853d-bc40646c431e.png)  
  
'eye'(또는 카메라를 생각하여 카메라 중심)을 (0,0,0)에 배치한다. y축은 위로, x축은 오른쪽으로 이동한다.  
화면의 왼쪽 끝에서 화면을 가로 지르고 화면 양쪽을 따라 두 개의 오프셋 벡터를 사용하여 화면에서 광선 끝 점을 이동한다.

## Ray Example
```csharp

using UnityEngine;

void RayExample()
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
```

* 색상 메소드는 y좌표의 위/아래에 따라 흰색과 파란색을 선형으로 혼합한다.  
* 먼저 -1.0 < y <1.0의 단위 벡터를 만들고, 다음 표준 그래픽 트릭을 0.0 < t <1.0으로 조정한다.   
* t = 1.0일 때 파란색, t = 0.0일 때 흰색이 되며, 그 사이는 혼합된다.  
* 이것은 두 가지 사이의 '선형 혼합' 또는 '선형 보간법' 또는 간단히 'lerp'를 형성한다.  
* lerp는 항상 blended_value = (1-t) * start_value + t * end_value와 같은 형태이며, t의 범위는 0 <= t <= 1 이다.  
* 위와 같은 코드를 실행하여, PPM Viewer로 파일을 열어보면 다음과 같은 이미지를 확인할 수 있다.  
![Ray Example](https://user-images.githubusercontent.com/15705675/50885697-c4401d80-1431-11e9-9b7a-87d01bc08ae0.png)
