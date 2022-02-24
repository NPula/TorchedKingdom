using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "";
    public string transitionAreaName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (sceneToLoad != "")
            {
                // TODO - Change the way scene loading works later.
                PlayerController.Instance.TransitionName = transitionAreaName;

                // Change Scene
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
