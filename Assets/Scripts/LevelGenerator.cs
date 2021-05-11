using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Ceiling;

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

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialRoom();
        if(DoorNorth)
        {
            YOffset = RoomY*2;
            XOffset += DoorPlace*2;
            
        }
        else
        {
            XOffset = RoomX * 2;
            YOffset += DoorPlace * 2;
        }

        for(int i = 0; i < NumberOfRoomsToGenerate - 1; i++)
        {
            bool newDoorNorth = (Random.Range(0f, 1f) > 0.5f);
            RoomX = Random.Range(MinimumRoomSize, MaximumRoomSize + 1);
            RoomY = Random.Range(MinimumRoomSize, MaximumRoomSize + 1);
            RoomZ = Random.Range(MinimumRoomSize, MaximumRoomSize + 1);
            Room newRoom = new Room(XOffset, YOffset, RoomX, RoomY, RoomZ,
                Floor, Wall, Ceiling, newDoorNorth, DoorNorth);
            newRoom.DrawSelf();
            DoorPlace = newRoom.ReturnDoorPlace();
            YOffset = newRoom.ReturnOffsetY();
            XOffset = newRoom.ReturnOffsetX();
            if(newDoorNorth)
            {
                YOffset += RoomY * 2;
                XOffset += DoorPlace * 2;
            }
            else
            {
                XOffset += RoomX * 2;
                YOffset += DoorPlace * 2;
            }
            DoorNorth = newDoorNorth;

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
                floorDraw.transform.position = new Vector3(i * 2, 0, j * 2);
                if (i == 0) // west walls
                {
                    for (int k = 0; k < RoomZ; k++)
                    {
                        GameObject wallDraw = Instantiate(Wall);
                        wallDraw.transform.position = new Vector3(i * 2, k * 2, j * 2);
                        wallDraw.transform.Rotate(0, 90, 0);
                    }
                }
                if (j == 0) // south walls
                    for (int k = 0; k < RoomZ; k++)
                    {
                        GameObject wallDraw = Instantiate(Wall);
                        wallDraw.transform.position = new Vector3(i * 2, k * 2, j * 2);
                        wallDraw.transform.Rotate(0, 0, 0);
                    }
                if (i == RoomX - 1)
                { // east walls
                    for (int k = 0; k < RoomZ; k++)
                    {
                        if (DoorNorth || (!DoorNorth && j + 1 != DoorPlace) || k!=0)
                        {
                            GameObject wallDraw = Instantiate(Wall);
                            wallDraw.transform.position = new Vector3(i * 2, k * 2, j * 2);
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
                            wallDraw.transform.position = new Vector3(i * 2, k * 2, j * 2);
                            wallDraw.transform.Rotate(0, 180, 0);
                        }
                    }
                }
                GameObject ceilDraw = Instantiate(Ceiling);
                ceilDraw.transform.position = new Vector3(i * 2, (RoomZ - 1) * 2, j * 2);

            }
        }
    }
}
