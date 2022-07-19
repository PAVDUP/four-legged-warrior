using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMake : MonoBehaviour
{
    Queue<int> roomMaker; //���� ���� �� ���
    Queue<int> trashCan; //roomMaker���� ���� ������ �� �� ���� ������ �� �ֵ���
    Room[] madedRoom; //���� ���� ����
    int roomCounter = 7; //���� �� ���� >> ���� ���� �߰��� ��
    private void Start()
    {
        roomMaker= new Queue<int>();
        trashCan = new Queue<int>();
        madedRoom= new Room[100];
        for(int i = 0; i < madedRoom.Length; i++)
        {
            madedRoom[i].cellNum = i;
        }
        roomMaker.Enqueue(44); // 44 == ���� ��
        ActivateRoom(44);
        int count = 1;
        while (count < roomCounter)
        {
            if (roomMaker.Count == 0) // �� ������ ������ ����� ����, ���� ��ǥ �� ������ �������� ���ߴٸ�
            {
                foreach(int number in trashCan)
                {
                    roomMaker.Enqueue(number);//�̹� �������(�� ���������� �ѹ� ���Ե�) ����� �ٽ� ����
                }
            } // ���� �� ���ߴµ� roomMaker�� �������
            var i = roomMaker.Dequeue();
            if(!trashCan.Contains(i)) trashCan.Enqueue(i);
            if (!madedRoom[i+1].isActive)
            {
                int rndm = Random.Range(0, 2);
                if(rndm == 0)
                {
                    roomMaker.Enqueue(i + 1);
                    ActivateRoom(i + 1);
                    count++;
                }
            } // ���� ��.x + 1
            if (count >= roomCounter) break;
            if (!madedRoom[i - 1].isActive)
            {
                int rndm = Random.Range(0, 2);
                if (rndm == 0)
                {
                    roomMaker.Enqueue(i - 1);
                    ActivateRoom(i - 1);
                    count++;
                }
            } // ���� ��.x - 1
            if (count >= roomCounter) break;
            if (!madedRoom[i + 10].isActive)
            {
                int rndm = Random.Range(0, 2);
                if (rndm == 0)
                {
                    roomMaker.Enqueue(i + 10);
                    ActivateRoom(i + 10);
                    count++;
                }
            } // ���� ��.y + 1
            if (count >= roomCounter) break;
            if (!madedRoom[i - 10].isActive)
            {
                int rndm = Random.Range(0, 2);
                if (rndm == 0)
                {
                    roomMaker.Enqueue(i - 10);
                    ActivateRoom(i - 10);
                    count++;
                }
            } // ���� ��.y - 1
            if (count >= roomCounter) break;
        }
    }

    void ActivateRoom(int num)
    {
        madedRoom[num].isActive = true;
        madedRoom[num+1].leftactive = true;
        madedRoom[num+1].boundaryActive++;
        madedRoom[num-1].rightactive = true;
        madedRoom[num-1].boundaryActive++;
        madedRoom[num+10].downactive = true;
        madedRoom[num+10].boundaryActive++;
        madedRoom[num-10].upactive = true;
        madedRoom[num-10].boundaryActive++;
    }
}
