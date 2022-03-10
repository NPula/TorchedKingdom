using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Image m_imageToFade;
    [SerializeField] GameObject menu;

    private static MenuManager m_instance;
    public static MenuManager Instance { get { return m_instance; } }

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, lvlText, xpText;

    [SerializeField] Slider[] xpSliders;
    [SerializeField] Image[] characterImages;
    [SerializeField] GameObject[] characterPanels;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (menu.activeInHierarchy)
            {
                UpdateStats();
                menu.SetActive(false);
                GameManager.Instance.gameMenuOpened = false;
            }
            else
            {
                menu.SetActive(true);
                GameManager.Instance.gameMenuOpened = true;
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.Instance.GetPlayerStats();

        for (int i = 0; i < playerStats.Length; i++)
        {
            characterPanels[i].SetActive(true);
        }
    }

    public void FadeImage()
    {
        Animator animator = m_imageToFade.GetComponent<Animator>();
        animator.SetTrigger("startFade");
    }
}
