using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLeave : MonoBehaviour
{
    public string direction;
    public bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Pass the direction we want to move into the event functions.
            EventManager.TriggerEvent("OnMapExit", new Dictionary<string, object> { { "direction", direction } });
        }
    }
}
