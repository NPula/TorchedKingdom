using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private string m_playerName;

    [SerializeField] int maxLevel = 50;
    [SerializeField] private int m_playerLevel = 1;
    [SerializeField] private int m_currentExp;
    [SerializeField] int[] xpForNextLevel;
    [SerializeField] int baseLevelXP = 100;

    [SerializeField] private int m_maxHP = 100;
    [SerializeField] private int m_currentHP;
    
    [SerializeField] private int m_maxMana = 30;
    [SerializeField] private int m_currentMana;
    
    [SerializeField] private int m_dexterity;
    [SerializeField] private int m_defence;

    private void Start()
    {
        xpForNextLevel = new int[maxLevel];
        xpForNextLevel[0] = 0;
        xpForNextLevel[1] = baseLevelXP;

        for (int i = 2; i < xpForNextLevel.Length; i++)
        {
            xpForNextLevel[i] = (int)(0.02f * i * i * i + 3.06f * i * i * i + 105.6f * i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddXP(100);
        }
    }

    public void AddXP(int amountOfXp)
    {
        m_currentExp += amountOfXp;
        if (m_currentExp > xpForNextLevel[m_playerLevel])
        {
            m_currentExp -= xpForNextLevel[m_playerLevel];
            m_playerLevel++;

            m_maxHP = (int)(m_maxHP * 1.06);
            m_currentHP = m_maxHP;
            
            m_maxMana = (int)(m_maxHP * 1.06);
            m_currentMana = m_maxMana;

            if (m_playerLevel % 2 == 0)
            {
                m_dexterity++;
            }
            else
            {
                m_defence++;
            }
        }
    }
}
