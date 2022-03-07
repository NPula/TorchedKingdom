using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    // Tilemap to confine player to.
    private Tilemap m_tilemap;

    // prefabs for each tilemap level layout.
    // TODO - Will probably need a different list for each piece type.
    [SerializeField] private List<GameObject> m_levelPieces;
    private List<Tilemap> tilemaps;

    [SerializeField] private GameObject tilemapWallUp;
    [SerializeField] private GameObject tilemapWallRight;
    [SerializeField] private GameObject tilemapWallLeft;
    [SerializeField] private GameObject tilemapWallDown;


    // Area the player is confined to.
    [SerializeField] private PolygonCollider2D m_playerBounds;
    [SerializeField] private Tilemap m_mapBounds;

    // Offset used to keep player in the play area.
    [SerializeField] private float m_offsetX = 0f;
    [SerializeField] private float m_offsetY = 0f;

    // Rectangle for play area. Used to confine player to play space.
    // Also contains the tilemap size for level generation stuff.
    private Vector3 m_bottomLeftEdge;
    private Vector3 m_topRightEdge;
    private Vector3 m_mapSize;

    // Pseudo random level generator
    private LevelGenerator m_levelGen;

    private void Start()
    {
        // Generate a pseudo random array for level generation
        m_levelGen = new LevelGenerator();
        m_levelGen.CreateRandomPath();

        // Used for grabbing the size of the first instantiated tilemap
        // Creating an object just to destroy it is kinda shit but it works.
        //GameObject go = Instantiate(m_levelPieces[0]);
        //Bounds tilemapBounds= go.transform.Find("Ground").GetComponent<Tilemap>().localBounds;
        //Destroy(go);

        Bounds tileBounds = m_mapBounds.localBounds;

        // Confine player to tilemap size
        // TODO - might need to change with the addition of random map layouts.
        //m_bottomLeftEdge = tilemapBounds.min + new Vector3(m_offsetX, m_offsetY, 0f);
        //m_topRightEdge = tilemapBounds.max + new Vector3(-m_offsetX, -m_offsetY, 0f);

        m_bottomLeftEdge = tileBounds.min + new Vector3(m_offsetX, m_offsetY, 0f);
        m_topRightEdge = tileBounds.max + new Vector3(-m_offsetX, -m_offsetY, 0f);


        //m_bottomLeftEdge = m_playerBounds.bounds.min + new Vector3(m_offsetX, m_offsetY, 0f);
        //m_topRightEdge = m_playerBounds.bounds.max + new Vector3(-m_offsetX, -m_offsetY, 0f);

        // Total size of tilemap
        m_mapSize = m_topRightEdge - m_bottomLeftEdge;

        // Prevent player from moving outside the map bounds or coordinates.
        PlayerController.Instance.SetMoveLimit(m_bottomLeftEdge, m_topRightEdge);

        SpawnRooms();
    }

    private void SpawnRooms()
    {
        GameObject parent = new GameObject("Level Parent");

        // Run through the 2d array in level generator
        // starting from the back of the array.
        for (int i = m_levelGen.sizeY-1; i >= 0; i--)
        {
            for (int j = (m_levelGen.sizeX-1); j >= 0; j--)
            {
                if (m_levelGen.rooms[i, j].isUsed)
                {
                    // TODO - Get random room piece from list.
                    // Testing without this for now.
                    int index = 0;

                    // the '-'m_mapSize.y just flips the y-position so the map generated matches the rooms array.
                    // we need to multiply by '-1' because we are looping starting at the back of the array.
                    // subtract the starting position ((m_levelGen.sizeX - 1) / 2), (m_levelGen.sizeY - 1)) to move
                    // the rooms to the center of unity's grid.

                    // TODO - Enable this code and comment out the lines underneath eventually. This is has the mapsize-2.
                    // Which creates the maps flush with one another. This causes the player to walk into the 'arealeave trigger' for both maps.
                    // we should fix this later. Another way is to construct the maps around this limitation which might be disirable anyway
                    // since why have overlapping tiles if you cant see some anyway. Seems kinda wasteful.
                    //float newLocX = ((m_mapSize.x - 2) * j) - (((m_levelGen.sizeX - 1) / 2) * (m_mapSize.x - 2));
                    //float newLocY = (-(m_mapSize.y - 2) * i) + ((m_levelGen.sizeY - 1) * (m_mapSize.y - 2));
                    
                    float newLocX = ((m_mapSize.x-1) * j) - (((m_levelGen.sizeX-1) / 2) * (m_mapSize.x-1));
                    float newLocY = (-(m_mapSize.y-1) * i) + ((m_levelGen.sizeY-1) * (m_mapSize.y-1));

                    // Instantiate piece
                    GameObject go = Instantiate(m_levelPieces[index], new Vector3(newLocX, newLocY), Quaternion.identity);
                    go.transform.parent = parent.transform;

                    // This only happens once on load so who cares.
                    // We should find an easier way of managing this later. This method makes it
                    // annoying when making changes to objects
                    GameObject wallUp = go.transform.Find("DoorSlots").Find("UpperWall").gameObject;
                    GameObject wallDown = go.transform.Find("DoorSlots").Find("BottomWall").gameObject;
                    GameObject wallRight = go.transform.Find("DoorSlots").Find("RightWall").gameObject;
                    GameObject wallLeft = go.transform.Find("DoorSlots").Find("LeftWall").gameObject;

                    if (m_levelGen.rooms[i, j].connections["Up"])
                    {
                        wallUp.SetActive(false);
                        Debug.Log("Called");
                    }
                    if (m_levelGen.rooms[i, j].connections["Down"])
                    {
                        wallDown.SetActive(false);
                    }
                    if (m_levelGen.rooms[i, j].connections["Left"])
                    {
                        wallLeft.SetActive(false);
                    }
                    if (m_levelGen.rooms[i, j].connections["Right"])
                    {
                        wallRight.SetActive(false);
                    }
                }
            }
        }
    }
}
