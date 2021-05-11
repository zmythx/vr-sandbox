using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    int OffsetX;
    int OffsetY;
    int RoomX;
    int RoomY;
    int PreviousDoorplace;
    int DoorPlace;
    int RoomZ;
    int CeilingHeight;
    bool PreviousDoorNorth;
    bool DoorNorth;
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Ceiling;
    public Room(int offx, int offy, int roomx, int roomy, int roomz, GameObject floor, GameObject wall, GameObject ceiling, bool doornorth, bool prevdoornorth)
    {
        OffsetX = offx;
        OffsetY = offy;
        RoomX = roomx;
        RoomY = roomy;
        RoomZ = roomz;
        Floor = floor;
        Ceiling = ceiling;
        Wall = wall;
        DoorNorth = doornorth;
        PreviousDoorNorth = prevdoornorth;
    }
    public int ReturnOffsetX()
    {
        return OffsetX;
    }
    public int ReturnOffsetY()
    {
        return OffsetY;
    }
    public int ReturnDoorPlace()
    {
        return DoorPlace;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DrawSelf()
    {
        int prevXOffset = OffsetX;
        int prevYOffset = OffsetY;

        if (DoorNorth)
        {
            DoorPlace = Random.Range(1, RoomX + 1);
        }
        else
        {
            DoorPlace = Random.Range(1, RoomY + 1);
        }
        if(PreviousDoorNorth)
        {
            OffsetX -= (Random.Range(1, RoomX) * 2);
        }
        else
        {
            OffsetY -= (Random.Range(1, RoomY) * 2);
        }
        for (int i = 0; i < RoomX; i++)
        {
            for (int j = 0; j < RoomY; j++)
            {
                GameObject floorDraw = Instantiate(Floor);
                floorDraw.transform.position = new Vector3(i * 2 + OffsetX, 0, j * 2 + OffsetY);
                if (i == 0) // west walls
                {
                    for (int k = 0; k < RoomZ; k++)
                    {
                        if (PreviousDoorNorth || (!PreviousDoorNorth && (j + 1) * 2 + OffsetY != prevYOffset) || k != 0)
                        {
                            GameObject wallDraw = Instantiate(Wall);
                            wallDraw.transform.position = new Vector3(i * 2 + OffsetX, k * 2, j * 2 + OffsetY);
                            wallDraw.transform.Rotate(0, 90, 0);
                        }
                    }
                }
                if (j == 0) // south walls
                    for (int k = 0; k < RoomZ; k++)
                    {
                        if (!PreviousDoorNorth || (PreviousDoorNorth && (i + 1) * 2 + OffsetX != prevXOffset) || k != 0)
                        {
                            GameObject wallDraw = Instantiate(Wall);
                            wallDraw.transform.position = new Vector3(i * 2 + OffsetX, k * 2, j * 2 + OffsetY);
                            wallDraw.transform.Rotate(0, 0, 0);
                        }
                    }
                if (i == RoomX - 1)
                { // east walls
                    for (int k = 0; k < RoomZ; k++)
                    {
                        if (DoorNorth || (!DoorNorth && j + 1 != DoorPlace) || k != 0)
                        {
                            GameObject wallDraw = Instantiate(Wall);
                            wallDraw.transform.position = new Vector3(i * 2 + OffsetX, k * 2, j * 2 + OffsetY);
                            wallDraw.transform.Rotate(0, 270, 0);
                        }
                    }
                }
                if (j == RoomY - 1) //north walls
                {
                    for (int k = 0; k < RoomZ; k++)
                    {
                        if (!DoorNorth || (DoorNorth && i + 1 != DoorPlace) || k != 0)
                        {
                            GameObject wallDraw = Instantiate(Wall);
                            wallDraw.transform.position = new Vector3(i * 2 + OffsetX, k * 2, j * 2 + OffsetY);
                            wallDraw.transform.Rotate(0, 180, 0);
                        }
                    }
                }
                GameObject ceilDraw = Instantiate(Ceiling);
                ceilDraw.transform.position = new Vector3(i * 2 + OffsetX, (RoomZ - 1) * 2, j * 2 + OffsetY);

            }
        }
    }
}
