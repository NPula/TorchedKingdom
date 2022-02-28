using System;
using System.IO;
using UnityEngine;

// Defines a room to be added to a list
public struct Room
{
    public bool isUsed;

    public Room(bool used)
    {
        isUsed = used;
    }
}

// NOT USING AT THE MOMENT.
// possible tiles to place. 
// we dont need to move down for this.
public enum Moves
{
    UP,
    LEFT,
    RIGHT
}

public class LevelGenerator 
{
    // Number of rooms to generate
    private int m_maxRoomAmount = 100;

    // Room list size
    public int sizeX = 50;
    public int sizeY = 50;

    // Room list
    public Room[,] rooms;

    public LevelGenerator()
    {
        // Set all rooms in list to be unused.
        InitializeRoomList();
    }

    // Creates a room list with all rooms set to false or unused.
    public void InitializeRoomList()
    {
        rooms = new Room[sizeY, sizeX];

        Room defRoom = new Room(false);
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                rooms[i, j] = defRoom;
            }
        }
    }

    // TODO - Doesn't really work for alot of rooms with a smaller array size. 
    //        Maybe change to fix this since it only creates about 6 rooms with 4x4 array.

    // Create a simple path through the array
    public void CreateRandomPath()
    {
        // Set initial slot to be center bottom in the array.
        int indY = (sizeY - 1);
        int indX = (sizeX - 1) / 2;

        // Set the initial slot to be used.
        rooms[indY, indX].isUsed = true;

        // The direction to add room to.
        int direction = 0;

        // Place n amount of rooms in the array
        for (int i = 0; i < m_maxRoomAmount; i++)
        {

            // while the move is valid 
            bool isValid = false;
            while (!isValid)
            {
                // Set this initally to true
                isValid = true;

                // Get a random direction to move through the list
                // TODO - should change this so this class doesnt rely on Unity
                direction = UnityEngine.Random.Range(0, 3);

                // Move up
                if (direction == 0)
                {
                    indY = ((indY - 1) < 0) ? indY : --indY;
                }
                // Move left
                else if (direction == 1)
                {
                    indX = ((indX - 1) < 0) ? indX : --indX;
                }
                // Move right
                else if (direction == 2)
                {
                    indX = ((indX + 1) >= sizeX) ? indX : ++indX;
                }

                // make sure the room isnt already being used.
                if (rooms[indY, indX].isUsed)
                {
                    isValid = false;
                }

                // if no more moves available just return for now. Should find a different way of handling this
                // to ensure there are enough rooms created.
                if (!MovesAvailable(indY, indX))
                {
                    i = m_maxRoomAmount;
                    break;
                }
            }

            // Set the new room to used.
            rooms[indY, indX].isUsed = true;
        }

        // Print the room layout to file here
        // C:\Users\Nick\AppData\LocalLow\DefaultCompany\TorchedKingdom\ LeveArray.txt
        PrintToFile();
    }

    private bool MovesAvailable(int currPosY, int currPosX)
    {
        int checkTop = ((currPosY - 1) < 0) ? currPosY : --currPosY;
        int checkLeft = ((currPosX - 1) < 0) ? currPosX : --currPosX;
        int checkRight = ((currPosX + 1) >= sizeX) ? currPosX : ++currPosX;
        
        if (rooms[checkTop, currPosX].isUsed && 
            rooms[currPosY, checkLeft].isUsed && 
            rooms[currPosY, checkRight].isUsed)
        {
            return false;
        }

        return true;
    }

    // Print for debugging
    public void PrintToFile()
    {
        // using will close the files automatically when we don't need it.
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/LevelArray.txt"))
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    sw.Write(rooms[i, j].isUsed.ToString()[0] + " ");
                }
                sw.Write(Environment.NewLine);
            }
        }
    }

    public void AppendToFile(string data)
    {
        // using will close the files automatically when we don't need it.
        using (StreamWriter sw = File.AppendText(Application.persistentDataPath + "/LevelArray.txt"))
        {
            sw.Write(Environment.NewLine);
            sw.WriteLine(data);
        }
    }
}
