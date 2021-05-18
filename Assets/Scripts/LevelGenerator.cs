using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Ceiling;
    public GameObject Door;

    public int NumberOfRoomsToGenerate = 5;
    public int MinimumRoomSize = 3;
    public int MaximumRoomSize = 8;
    public int MinimumCeiling = 2;
    public int MaximumCeiling = 4;

    int XOffset = 0;
    int YOffset = 0;

    int RoomX;
    int RoomY;
    int RoomZ;
    bool DoorNorth;
    int DoorPlace;

    
    public GameObject DebugEnemy;
    public int DebugEnemySpawns = 5;
    public bool DebugMode;

    public int AssetSquareSize = 4;

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialRoom();
        if(DoorNorth)
        {
            YOffset = RoomY * AssetSquareSize;
            XOffset += DoorPlace * AssetSquareSize;
            
        }
        else
        {
            XOffset = RoomX * AssetSquareSize;
            YOffset += DoorPlace * AssetSquareSize;
        }

        for(int i = 0; i < NumberOfRoomsToGenerate - 1; i++)
        {
            bool newDoorNorth = (Random.Range(0f, 1f) > 0.5f);
            RoomX = Random.Range(MinimumRoomSize, MaximumRoomSize + 1);
            RoomY = Random.Range(MinimumRoomSize, MaximumRoomSize + 1);
            RoomZ = Random.Range(MinimumCeiling, MaximumCeiling + 1);
            Room newRoom = new Room(XOffset, YOffset, RoomX, RoomY, RoomZ,
                Floor, Wall, Ceiling, Door, newDoorNorth, DoorNorth, AssetSquareSize);
            newRoom.DrawSelf();
            DoorPlace = newRoom.ReturnDoorPlace();
            YOffset = newRoom.ReturnOffsetY();
            XOffset = newRoom.ReturnOffsetX();
            int xOldOffset = XOffset;
            int yOldOffset = YOffset;
            
            if(newDoorNorth)
            {
                YOffset += RoomY * AssetSquareSize;
                XOffset += DoorPlace * AssetSquareSize;
            }
            else
            {
                XOffset += RoomX * AssetSquareSize;
                YOffset += DoorPlace * AssetSquareSize;
            }
            DoorNorth = newDoorNorth;
            if (DebugMode)
            {
                for (int j = 0; j < DebugEnemySpawns; j++)
                {
                    GameObject debugEnemy = Instantiate(DebugEnemy);
                    debugEnemy.transform.position = new Vector3(Random.Range(xOldOffset + 2f, XOffset - 2f), Random.Range(1f, RoomZ * AssetSquareSize - AssetSquareSize - 1f), Random.Range(yOldOffset + 2f, YOffset - 2f));
                    newRoom.DrawnDoor.transform.GetComponent<ShieldDoor>().EnemyArray.Add(debugEnemy);
                    Debug.Log("Adding enemy to shield door at " + newRoom.DrawnDoor.transform.position);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateInitialRoom()
    {
        RoomX = Random.Range(MinimumRoomSize, MaximumRoomSize + 1);
        RoomY = Random.Range(MinimumRoomSize, MaximumRoomSize + 1);
        RoomZ = Random.Range(MinimumCeiling, MaximumCeiling + 1);
        DoorNorth = false;
        if(Random.Range(0, 1f) > 0.5f)
        {
            DoorNorth = true;
        }
        int CeilingHeight = Random.Range(MinimumCeiling, MaximumCeiling + 1);
        if(DoorNorth)
        {
            DoorPlace = Random.Range(1, RoomX + 1);
        }
        else
        {
            DoorPlace = Random.Range(1, RoomY + 1);
        }
        for(int i = 0; i < RoomX; i++)
        {
            for (int j = 0; j < RoomY; j++)
            {
                GameObject floorDraw = Instantiate(Floor);
                floorDraw.transform.position = new Vector3(i * AssetSquareSize, 0, j * AssetSquareSize);
                if (i == 0) // west walls
                {
                    for (int k = 0; k < RoomZ; k++)
                    {
                        GameObject wallDraw = Instantiate(Wall);
                        wallDraw.transform.position = new Vector3(i * AssetSquareSize, k * AssetSquareSize, j * AssetSquareSize);
                        wallDraw.transform.Rotate(0, 90, 0);
                    }
                }
                if (j == 0) // south walls
                    for (int k = 0; k < RoomZ; k++)
                    {
                        GameObject wallDraw = Instantiate(Wall);
                        wallDraw.transform.position = new Vector3(i * AssetSquareSize, k * AssetSquareSize, j * AssetSquareSize);
                        wallDraw.transform.Rotate(0, 0, 0);
                    }
                if (i == RoomX - 1)
                { // east walls
                    for (int k = 0; k < RoomZ; k++)
                    {
                        if (DoorNorth || (!DoorNorth && j + 1 != DoorPlace) || k!=0)
                        {
                            GameObject wallDraw = Instantiate(Wall);
                            wallDraw.transform.position = new Vector3(i * AssetSquareSize, k * AssetSquareSize, j * AssetSquareSize);
                            wallDraw.transform.Rotate(0, 270, 0);
                        }
                    }
                }
                if(j == RoomY - 1) //north walls
                {
                    for (int k = 0; k < RoomZ; k++)
                    {
                        if (!DoorNorth || (DoorNorth && i + 1 != DoorPlace) || k!=0)
                        {
                            GameObject wallDraw = Instantiate(Wall);
                            wallDraw.transform.position = new Vector3(i * AssetSquareSize, k * AssetSquareSize, j * AssetSquareSize);
                            wallDraw.transform.Rotate(0, 180, 0);
                        }
                    }
                }
                GameObject ceilDraw = Instantiate(Ceiling);
                ceilDraw.transform.position = new Vector3(i * AssetSquareSize, (RoomZ - 1) * AssetSquareSize, j * AssetSquareSize);

            }
        }
    }
}
