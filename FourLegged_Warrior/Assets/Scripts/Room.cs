using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int cellNum = 0; // ���� ��ǥ 10���ڸ� y 1���ڸ� x �����ϴ� ��ġ 44
    public bool isActive = false; //���� �� ���ΰ�
    public int boundaryActive = 0; //���� 4�ڸ��� ���ڸ��� active �Ǿ��°�
    public bool leftactive = false, rightactive = false, upactive = false, downactive = false;
    int xMin, xMax;
    int yMin, yMax;
    int theme;//���� ����

}
