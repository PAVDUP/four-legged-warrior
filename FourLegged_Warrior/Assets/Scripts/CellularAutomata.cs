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
    void GenerateMap() //���� �����ϴ� �ڵ�
    {
        map = new int[width, height]; //���� ���̸� �Է¹��� 2���� �迭 int��
        RandomFillMap(); //�����ϰ� ���̳� ������ ä���.(1)
        for(int i = 0;i < 5; i++)
        {
            SmoothMap();//��źȭ �۾��� �Ѵ�(2)
        }
        InstantiateMap();//���� ������ ���� �����Ѵ�(3)
    }
    void RandomFillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0||y == height - 1)//�ܰ��� ������ ó���Ѵ�.
                {
                    map[x, y] = 1;
                }
                else
                    map[x, y] = UnityEngine.Random.Range(0, 100) < randomFillPercent ? 1 : 0;//���δ� ���� Ȯ���� ��,�� �����Ѵ�.
            }
        }
    }
    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);//������ ���� �����ش�.
                if (neighbourWallTiles > 4)//������ ���� 4�� �ʰ��� ������
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)//�̸��̸� ������ �������ش�.
                    map[x, y] = 0;
            }
        }
    }
    int GetSurroundingWallCount(int gridX,int gridY)
    {
        int wallCount = 0;
        for(int neighbourX =gridX-1;neighbourX <= gridX + 1; neighbourX++)//�߰� Ÿ���� �������� 3x3�� Ȯ���Ѵ�.
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)//���� ���� �ȿ���
                {
                    if (neighbourX != gridX || neighbourY != gridY)//�ڱ� �ڽ��� ���� ó������ ���� ���̰�
                    {
                        wallCount += map[neighbourX, neighbourY];//Ȯ���� Ÿ���� ���ΰ�� map[,]�� ���� 1�̹Ƿ� ��� �����ش�
                    }
                }
                else
                    wallCount++;//���� ���̸� ��� ���ؼ� �׳� ������ ����� �ش�.
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
            for (int y = 0; y < height; y++){//���� �ȿ��� ���� �������ش�.
                Vector3 vec3 = new Vector3(x,y,0);//��ǥ �޾Ƽ�
                if(map[x, y] == 1)
                {
                    Instantiate(walltile,vec3,Quaternion.identity);//����
                }
                else if(map[x, y] == 0)
                {
                    Instantiate(roomtile,vec3,Quaternion.identity);//����
                }
            }
        }
    }
}
