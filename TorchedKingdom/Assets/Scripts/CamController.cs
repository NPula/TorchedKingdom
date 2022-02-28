using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CamController : MonoBehaviour
{
    // player we want to follow
    private PlayerController playerTarget;

    // reference to the cinemachine camera and extensions
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner confiner;

    // Object we want to confine the camera to
    public GameObject cameraBoundsObject;
    private PolygonCollider2D cameraBoundsCollider;

    void Start()
    {
        EventManager.StartListening("OnMapExit", OnNewArea);

        playerTarget = FindObjectOfType<PlayerController>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();

        virtualCamera.Follow = playerTarget.transform;

        // Get and set the collider used to confine the camera to some region.
        cameraBoundsCollider = cameraBoundsObject.GetComponent<PolygonCollider2D>();
        confiner.m_BoundingShape2D = cameraBoundsCollider;
    }

    // called when player moves to a new map area.
    // Move the collider object to the next map
    void OnNewArea(Dictionary<string, object> message)
    {
        float newPosX = 0f;
        float newPosY = 0f;

        switch(message["direction"])
        {
            case "Up":
                newPosY = cameraBoundsCollider.bounds.size.y;
                break;
            case "Down":
                newPosY = -cameraBoundsCollider.bounds.size.y;
                break;
            case "Left":
                newPosX = -cameraBoundsCollider.bounds.size.x;
                break;
            case "Right":
                newPosX = cameraBoundsCollider.bounds.size.x;
                break;
        }

        cameraBoundsObject.transform.position += new Vector3(newPosX, newPosY);
    }
}
