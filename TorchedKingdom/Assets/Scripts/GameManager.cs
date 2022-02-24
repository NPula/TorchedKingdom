using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager Instance { get { return m_instance; } }

    private void Start()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Do stuff when a new scene loads
    }
}
