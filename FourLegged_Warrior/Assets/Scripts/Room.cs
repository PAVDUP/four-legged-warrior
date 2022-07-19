using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int cellNum = 0; // 방의 좌표 10의자리 y 1의자리 x 시작하는 위치 44
    public bool isActive = false; //생성 될 것인가
    public int boundaryActive = 0; //주위 4자리중 몇자리가 active 되었는가
    public bool leftactive = false, rightactive = false, upactive = false, downactive = false;
    int xMin, xMax;
    int yMin, yMax;
    int theme;//추후 변경

}
