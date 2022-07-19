using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMake : MonoBehaviour
{
    Queue<int> roomMaker; //방을 만들 때 사용
    Queue<int> trashCan; //roomMaker에서 값을 제거할 때 그 값을 저장할 수 있도록
    Room[] madedRoom; //만든 방을 저장
    int roomCounter = 7; //만들 방 갯수 >> 추후 조건 추가할 것
    private void Start()
    {
        roomMaker= new Queue<int>();
        trashCan = new Queue<int>();
        madedRoom= new Room[100];
        for(int i = 0; i < madedRoom.Length; i++)
        {
            madedRoom[i].cellNum = i;
        }
        roomMaker.Enqueue(44); // 44 == 시작 방
        ActivateRoom(44);
        int count = 1;
        while (count < roomCounter)
        {
            if (roomMaker.Count == 0) // 방 생성을 실행할 방들이 없고, 아직 목표 방 갯수에 도달하지 못했다면
            {
                foreach(int number in trashCan)
                {
                    roomMaker.Enqueue(number);//이미 만들어진(방 생성과정에 한번 투입된) 방들을 다시 투입
                }
            } // 생성 다 못했는데 roomMaker가 비었으면
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
            } // 현재 방.x + 1
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
            } // 현재 방.x - 1
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
            } // 현재 방.y + 1
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
            } // 현재 방.y - 1
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
