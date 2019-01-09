# Ch04 Adding a sphere
  
ray tracer의 ray가 구형에 닿는지 여부를 계산하기 위해 단일 객체를 추가해보자.  
반경 R의 원점에 중심을 둔 구의 방정식은 x \* x + y \* y + z \* z = R \* R 이다.  
어떤 (x, y, z)에 대해서, x \* x + y \* y + z \* z = R \* R이면 (x, y, z)는 구에 있는 점이 된다.   

구면 중심이 (cx, cy, cz)에 있으면 더 이상해진다. :  
```
(x - cx) * (x - cx) + (y - cy) * (y - cy) + (z - cz) * (z - cz)= R * R  
```

중심 C = (cx, cy, cz)에서 점 p = (x, y, z)까지의 벡터는 (p - C)임을 알 수 있다.  
내적(p - C, p - c)은 (x - cx) \* (x - cx) \* (y - cy) \* (y - cy) + (z - cz) \* (z - cz)가 된다.  
따라서 벡터 형태의 구의 방정식은 다음과 같다.
```
dot(p - c, p - c) = R * R
```
이것을 "이 방정식을 만족하는 점 P는 구형에 있다"라고 읽을 수 있다.  
  
광선 p(t) = A + t \* B가 구의 어디에 닿는지 알고 싶다.  
광선에 구가 닿으면 p(t)가 구 방정식을 만족하는 어떤 t가 존재한다.  
t가 사실이라면 다음과 같은 식을 만족하는 값을 찾는다.  
```
dot ((A + t * B - C), ((p(t) - c) A + t * B - C)) = R * R   
```

위 식을 정리하면 다음과 같다.
```
t * t * dot(B, B) + 2 * t * dot(B, A - C) + dot(A - C, A - C) - R * R = 0
```

  
제곱근을 통해 t에 대해 알 수 있게 된다.
![discriminant](https://user-images.githubusercontent.com/15705675/50888091-3451a200-1438-11e9-9ccf-1e513fcc6875.png)
* 양수 : 두 개의 실제 해를 의미 함 
* 음수 : 실제 해를 의미하지 않음
* 0 : 하나의 실제 해를 의미 함


## Ray Hit Code
``` csharp
bool IsHittingOnSphere(Vector3 center, float radius, ref Ray ray)
{
    Vector3 oc = ray.origin - center;
    float a = Vector3.Dot(ray.direction, ray.direction);
    float b = 2f * Vector3.Dot(oc, ray.direction);
    float c = Vector3.Dot(oc, oc) - radius * radius;
    float d = b * b - 4 * a * c; // discriminant

    return (d > 0f);
}

Vector3 GetColor(ref Ray ray)
{
    Vector3 screen = new Vector3(0f, 0f, -1f);
    float radius = .5f;

    if(IsHittingOnSphere(screen, radius, ref ray))
    {
        return new Vector3(1f, 0f, 0f); // (255, 0, 0);
    }

    Vector3 eyePoint = new Vector3(.5f, .7f, 1f);
    Vector3 normalDir = ray.direction.normalized;
    float t = .5f * (normalDir.y + 1f);

    return (1f - t) * Vector3.one + t * eyePoint;
}
```

* 이미지 본문을 기록하는 코드는 [Ch03](https://github.com/yuriver/Ray-Tracing-Library-cpp-to-cs-/tree/master/Ch03/)와 같다.  
* 위와 같은 코드를 실행하여, PPM Viewer로 파일을 열어보면 다음과 같은 이미지를 확인할 수 있다.    
![ray hit_example](https://user-images.githubusercontent.com/15705675/50889108-7c71c400-143a-11e9-8770-87695f18ddbf.png)
