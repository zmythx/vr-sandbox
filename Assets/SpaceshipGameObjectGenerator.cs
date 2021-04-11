using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SpaceshipGameObjectGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private int[,] GameMapArray;
    private string[,] GameMapDescription;
    public enum HallwayLength
    {
        Short,
        Medium,
        Long,
        Mixed
    }
    public enum RoomSize
    {
        Small,
        Medium,
        Large,
        Mixed
    }
    public enum RoomHeight
    {
        Small,
        Medium,
        Large,
        Mixed
    }
    public enum HeightVariation
    {
        Small,
        Medium,
        Large,
    }
    public HallwayLength GenerateHallwayLength;
    public RoomSize GenerateRoomSize;
    public RoomHeight GenerateRoomHeight;
    public HeightVariation GenerateHeightVariation;

    public GameObject Floor;
    public GameObject Wall;
    public GameObject Ceiling;

    public int RoomsToGenerate = 3;
    private int roomHeight = 1;
    private int roomHeightVariation = 1;
    private int roomSize;
    private int roomSizeVariation = 1;
    private int hallwayLength;
    private int hallwayLengthVariation = 1;


    void Start()
    {
        switch(GenerateHeightVariation.ToString())
        {
            case "Small":
                roomHeightVariation = 1;
                break;
            case "Medium":
                roomHeightVariation = 3;
                break;
            case "Large":
                roomHeightVariation = 5;
                break;
        }
        switch(GenerateRoomSize.ToString())
        {
            case "Small":
                roomSize = 3;
                break;
            case "Medium":
                roomSize = 5;
                break;
            case "Large":
                roomSize = 7;
                break;
            case "Mixed":
                roomSize = 5;
                roomSizeVariation = 4;
                break;

        }
        switch(GenerateHallwayLength.ToString())
        {
            case "Small":
                hallwayLength = 3;
                break;
            case "Medium":
                hallwayLength = 7;
                break;
            case "Large":
                hallwayLength = 10;
                break;
            case "Mixed":
                hallwayLength = 7;
                hallwayLengthVariation = 4;
                break;

        }
        switch(GenerateRoomHeight.ToString())
        {
            case "Small":
                roomHeight = 1;
                break;
            case "Medium":
                roomHeight = 3;
                break;
            case "Large":
                roomHeight = 5;
                break;
            case "Mixed":
                roomHeight = 3;
                roomHeightVariation = 3;
                break;

        }
        int arraySize = ((roomSize + roomSizeVariation)) * RoomsToGenerate + 3;
        GameMapArray = new int[arraySize, arraySize];
        GameMapDescription = new string[arraySize, arraySize];
        System.Random rnd = new System.Random();
        for(int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                GameMapArray[i, j] = 0;
                GameMapDescription[i, j] = "Empty";
            }
        }
        for(int i = 0; i < RoomsToGenerate; i++)
        {
            int roomXstart = rnd.Next(0, arraySize - 1);
            int roomYstart = rnd.Next(0, arraySize - 1);
            int roomHeightGen = rnd.Next(roomHeight, roomHeightVariation + roomHeight);
            int roomXsize = roomSize + rnd.Next(0, roomSizeVariation);
            int roomYsize = roomSize + rnd.Next(0, roomSizeVariation);
            while(!CheckIfValidRoomPlacement(GameMapArray, GenerateRoom(roomXsize, roomYsize, roomHeightGen), roomXstart, roomYstart))
            {
                roomXstart = rnd.Next(0, arraySize - 1);
                roomYstart = rnd.Next(0, arraySize - 1);
                roomHeightGen = rnd.Next(roomHeight, roomHeightVariation + roomHeight);
                roomXsize = roomSize + rnd.Next(0, roomSizeVariation);
                roomYsize = roomSize + rnd.Next(0, roomSizeVariation);
            }
            AddRoom(GameMapArray, GenerateRoom(roomXsize, roomYsize, roomHeightGen), roomXstart, roomYstart, roomHeightGen, GameMapDescription);
        }
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < arraySize; i++)
        {
            sb.AppendLine();
            for(int j = 0; j < arraySize; j++)
            {
                sb.Append(GameMapArray[i, j].ToString());
            }

        }
        for (int i = 0; i < arraySize; i++)
        {
            sb.AppendLine();
            for (int j = 0; j < arraySize; j++)
            {
                sb.Append(GameMapDescription[i, j].ToString() + " ");
            }

        }
        Debug.Log(sb.ToString());
        DrawFloors(GameMapArray);
        DrawCeilings(GameMapArray);

    }
    private int[,] AddRoom(int[,] GameMap, int[,] RoomToAdd, int xStart, int yStart, int Height, string[,] GameMapDesc)
    {
        int RoomSizeX = RoomToAdd.GetLength(0);
        int RoomSizeY = RoomToAdd.GetLength(1);
        for(int i = xStart; i < xStart+RoomSizeX; i++)
        {
            for(int j = yStart; j < yStart+RoomSizeY; j++)
            {
                GameMap[i, j] = Height;
                GameMapDesc[i, j] = "Room";
            }
        }
        return GameMap;
    }
    private void DrawFloors(int[,] GameMap)
    {
        for (int i = 0; i < GameMap.GetLength(0); i++)
        {
            for (int j = 0; j < GameMap.GetLength(1); j++)
            {
                if (GameMap[i, j] > 0)
                {
                    GameObject floorTile = Instantiate(Floor);
                    floorTile.transform.parent = gameObject.transform;
                    floorTile.transform.position = new Vector3(i * 4, 0, j * 4);
                }
            }
        }
    }
    private void DrawCeilings(int[,] GameMap)
    {
        for (int i = 0; i < GameMap.GetLength(0); i++)
        {
            for (int j = 0; j < GameMap.GetLength(1); j++)
            {
                if (GameMap[i, j] > 0)
                {
                    GameObject floorTile = Instantiate(Ceiling);
                    floorTile.transform.parent = gameObject.transform;
                    floorTile.transform.position = new Vector3(i * 4, -4 + (GameMap[i, j] * 4), j * 4);
                }
            }
        }
    }
    private int[,] GenerateRoom(int RoomSizeX, int RoomSizeY, int Height)
    {
        int[,] room = new int[RoomSizeX, RoomSizeY];
        for(int i = 0; i < RoomSizeX; i++)
        {
            for(int j = 0; j < RoomSizeY; j++)
            {
                room[i, j] = Height;
            }
        }
        return room;
    }
    private bool CheckIfValidRoomPlacement(int[,] GameMap, int[,] RoomToAdd, int xStart, int yStart)
    {
        int RoomSizeX = RoomToAdd.GetLength(0);
        int RoomSizeY = RoomToAdd.GetLength(1);
        if(GameMap.GetLength(0) < xStart+RoomSizeX)
        {
            return false;
        }
        if(GameMap.GetLength(1) < yStart+RoomSizeY)
        {
            return false;
        }
        for(int i = xStart; i < xStart + RoomSizeX; i++)
        {
            for(int j = yStart; j < yStart + RoomSizeY; j++)
            {
                if(GameMap[i, j] != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
