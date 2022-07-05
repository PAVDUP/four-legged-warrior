using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CellularAutomata : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject walltile, roomtile;
    [Range (0,100)]
    public int randomFillPercent;
    [HideInInspector]
    public int[,] map;

    private void Start()
    {
        map = new int[width,height];
        GenerateMap();
    }
    void GenerateMap() //맵을 생성하는 코드
    {
        map = new int[width, height]; //폭과 넓이를 입력받은 2차원 배열 int형
        RandomFillMap(); //랜덤하게 벽이나 방으로 채운다.(1)
        for(int i = 0;i < 5; i++)
        {
            SmoothMap();//평탄화 작업을 한다(2)
        }
        InstantiateMap();//받은 정보로 맵을 구현한다(3)
    }
    void RandomFillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0||y == height - 1)//외곽은 벽으로 처리한다.
                {
                    map[x, y] = 1;
                }
                else
                    map[x, y] = UnityEngine.Random.Range(0, 100) < randomFillPercent ? 1 : 0;//내부는 일정 확률로 벽,방 결정한다.
            }
        }
    }
    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);//주위의 벽을 세어준다.
                if (neighbourWallTiles > 4)//주위에 벽이 4개 초과면 벽으로
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)//미만이면 방으로 변경해준다.
                    map[x, y] = 0;
            }
        }
    }
    int GetSurroundingWallCount(int gridX,int gridY)
    {
        int wallCount = 0;
        for(int neighbourX =gridX-1;neighbourX <= gridX + 1; neighbourX++)//중간 타일을 기준으로 3x3만 확인한다.
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)//맵의 범위 안에서
                {
                    if (neighbourX != gridX || neighbourY != gridY)//자기 자신의 값은 처리하지 않을 것이고
                    {
                        wallCount += map[neighbourX, neighbourY];//확인한 타일이 벽인경우 map[,]의 값은 1이므로 계속 더해준다
                    }
                }
                else
                    wallCount++;//범위 밖이면 계속 더해서 그냥 벽으로 만들어 준다.
            }
        }
        return wallCount;
    }
    /*private void OnDrawGizmos()
    {
        if(map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x,y]==1)?Color.black:Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, -height / 2 + y + .5f, 0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }*/
    void InstantiateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++){//범위 안에서 맵을 생성해준다.
                Vector3 vec3 = new Vector3(x,y,0);//좌표 받아서
                if(map[x, y] == 1)
                {
                    Instantiate(walltile,vec3,Quaternion.identity);//생성
                }
                else if(map[x, y] == 0)
                {
                    Instantiate(roomtile,vec3,Quaternion.identity);//생성
                }
            }
        }
    }
}
