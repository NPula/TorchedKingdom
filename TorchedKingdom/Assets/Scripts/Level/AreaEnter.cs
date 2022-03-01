using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEnter : MonoBehaviour
{
    public string transitionAreaName;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When loading new area fade the screen from black
        //MenuManager.Instance.FadeImage();

        MenuManager.Instance.FadeImage();

        if (transitionAreaName == PlayerController.Instance.TransitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
