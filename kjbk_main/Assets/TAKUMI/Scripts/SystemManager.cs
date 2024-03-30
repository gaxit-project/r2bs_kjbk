using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    static int MapWidth = 100;
    static int MapHeight = 100;

    public static int[,] Map;

    const int wall = 9;
    const int obstacle = 8;
    const int blaze = 7;
    const int road = 0;
    const int floar = -1;
    const int pl = -2;

    public GameObject WallObject;
    public GameObject FL1;
    public GameObject FL2;
    public GameObject PL;
    public GameObject Obstacle;
    public GameObject Blaze;

    int a;

    const int roomMinHeight = 94;
    const int roomMaxHeight = 95;

    const int roomMinWidth = 94;
    const int roomMaxWidth = 95;

    const int RoomCountMin = 1;
    const int RoomCountMax = 1;

    //道の集合点を増やしたいならこれを増やす
    const int meetPointCount = 10;

    void Start () {

        ResetMapDataFL();

        CreateFL();

        ResetMapData();

        CreateSpaceData();

        CreateDangeon();

    }

    /// <summary>
    /// Mapの二次元配列の初期化
    /// </summary>
    private void ResetMapDataFL() {
        Map = new int[MapHeight, MapWidth];
        for (int i = 0; i < MapHeight; i++) {
            for (int j = 0; j < MapWidth; j++) {
                Map[i, j] = floar;
            }
        }
    }
    private void ResetMapData() {
        Map = new int[MapHeight, MapWidth];
        for (int i = 0; i < MapHeight; i++) {
            for (int j = 0; j < MapWidth; j++) {
                Map[i, j] = wall;
            }
        }
    }

    /// <summary>
    /// 空白部分のデータを変更
    /// </summary>
    private void CreateSpaceData() {
        int roomCount = Random.Range(RoomCountMin, RoomCountMax);

        int[] meetPointsX = new int[meetPointCount];
        int[] meetPointsY = new int[meetPointCount];
        for (int i = 0; i < meetPointsX.Length; i++) {
            meetPointsX[i] = Random.Range(MapWidth / 4, MapWidth * 3 / 4);
            meetPointsY[i] = Random.Range(MapHeight / 4, MapHeight * 3 / 4);
            Map[meetPointsY[i], meetPointsX[i]] = road;
        }

        for (int i = 0; i < roomCount; i++) {
            int roomHeight = Random.Range(roomMinHeight, roomMaxHeight);
            int roomWidth = Random.Range(roomMinWidth, roomMaxWidth);
            int roomPointX = Random.Range(2, MapWidth - roomMaxWidth - 2);
            int roomPointY = Random.Range(2, MapWidth - roomMaxWidth - 2);

            int roadStartPointX = Random.Range(roomPointX, roomPointX + roomWidth);
            int roadStartPointY = Random.Range(roomPointY, roomPointY + roomHeight);

            bool isRoad = CreateRoomData(roomHeight, roomWidth, roomPointX, roomPointY);

            if (isRoad == false) {
                CreateRoadData(roadStartPointX, roadStartPointY, meetPointsX[Random.Range(0,0)], meetPointsY[Random.Range(0, 0)]);
            }
        }


    }

    /// <summary>
    /// 部屋データを生成。すでに部屋がある場合はtrueを返し、道を作らないようにする
    /// </summary>
    /// <param name="roomHeight">部屋の高さ</param>
    /// <param name="roomWidth">部屋の横幅</param>
    /// <param name="roomPointX">部屋の始点(x)</param>
    /// <param name="roomPointY">部屋の始点(y)</param>
    /// <returns></returns>
    private bool CreateRoomData(int roomHeight, int roomWidth, int roomPointX, int roomPointY) {
        bool isRoad = false;
        for (int i = 0; i < roomHeight; i++) {
            for (int j = 0; j < roomWidth; j++) {
                if (Map[roomPointY + i, roomPointX + j] == road) {
                    isRoad = true;
                } else {
                    Map[roomPointY + i, roomPointX + j] = road;
                }
            }
        }
        return isRoad;
    }

    /// <summary>
    /// 道データを生成
    /// </summary>
    /// <param name="roadStartPointX"></param>
    /// <param name="roadStartPointY"></param>
    /// <param name="meetPointX"></param>
    /// <param name="meetPointY"></param>
    private void CreateRoadData(int roadStartPointX, int roadStartPointY, int meetPointX, int meetPointY) {

        bool isRight;
        if (roadStartPointX > meetPointX) {
            isRight = true;
        } else {
            isRight = false;
        }
        bool isUnder;
        if (roadStartPointY > meetPointY) {
            isUnder = false;
        } else {
            isUnder = true;
        }

        if(Random.Range(0,2) == 0) {

            while (roadStartPointX != meetPointX) {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true) {
                    roadStartPointX--;
                } else {
                    roadStartPointX++;
                }

            }

            while(roadStartPointY != meetPointY) {

                Map[roadStartPointY, roadStartPointX] = road;
                if(isUnder == true) {
                    roadStartPointY++;
                } else {
                    roadStartPointY--;
                }
            }

        } else {

            while (roadStartPointY != meetPointY) {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isUnder == true) {
                    roadStartPointY++;
                } else {
                    roadStartPointY--;
                }
            }

            while (roadStartPointX != meetPointX) {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true) {
                    roadStartPointX--;
                } else {
                    roadStartPointX++;
                }

            }

        }
    }

    /// <summary>
    /// マップデータをもとにダンジョンを生成
    /// </summary>
    private void CreateDangeon() 
    {
        for (int i = 0; i < MapHeight; i++) 
        {
            for (int j = 0; j < MapWidth; j++) 
            {
                if (Map[i, j] == wall) 
                {
                    Instantiate(WallObject, new Vector3(j - MapWidth/2, 0, i - MapHeight/2), Quaternion.identity);
                }
            }
        }
    }
    private void CreateFL() 
    {
        for (int i = 0; i < MapHeight; i++) 
        {
            a = i % 2;
            for (int j = 0; j < MapWidth; j++) {
                if (Map[i, j] == floar) {
                    if(j % 2 == a)
                    {
                        Instantiate(FL1, new Vector3(j - MapWidth/2, -1, i - MapHeight/2), Quaternion.identity);
                    }else{
                        Instantiate(FL2, new Vector3(j - MapWidth/2, -1, i - MapHeight/2), Quaternion.identity);
                    }
                    
                }
            }
        }
    }
}
