using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerStats[] playerStats;

    public bool gameMenuOpened, dialogBoxOpened;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // Get all characters in game with player stats scripts attached.
        playerStats = FindObjectsOfType<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (gameMenuOpened || dialogBoxOpened)
        {
            PlayerController.Instance.DeactivateMovement = true;
        }
        else
        {
            PlayerController.Instance.DeactivateMovement = false;
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
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
