using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PolygonCollider2D))]
public class LevelBounds : MonoBehaviour
{
    [SerializeField] private Tilemap m_tilemap;

    void Awake()
    {
        // TODO - rename these to something easier to read.
        float boundsTopRightX = m_tilemap.localBounds.max.x;
        float boundsTopRightY = m_tilemap.localBounds.max.y;
        float boundsLowerLeftX = m_tilemap.localBounds.min.x;
        float boundsLowerLeftY = m_tilemap.localBounds.min.y;

        // set the collider position to match the tilemap position.
        PolygonCollider2D m_collider = GetComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[4];
        points[0] = new Vector2(boundsLowerLeftX, boundsLowerLeftY);
        points[1] = new Vector2(boundsLowerLeftX, boundsTopRightY);
        points[2] = new Vector2(boundsTopRightX, boundsTopRightY);
        points[3] = new Vector2(boundsTopRightX, boundsLowerLeftY);

        m_collider.points = points;
    }
}
