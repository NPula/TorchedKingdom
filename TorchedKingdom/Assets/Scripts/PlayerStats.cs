using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private string m_playerName;

    [SerializeField] int maxLevel = 50;
    [SerializeField] private int m_playerLevel = 1;
    [SerializeField] private int m_currentExp;
    [SerializeField] int[] xpForEachLevel;
    [SerializeField] int baseLevelXP = 100;

    [SerializeField] private int m_maxHP = 100;
    [SerializeField] private int m_currentHP;
    
    [SerializeField] private int m_maxMana = 30;
    [SerializeField] private int m_currentMana;
    
    [SerializeField] private int m_dexterity;
    [SerializeField] private int m_defence;

    private void Start()
    {
        xpForEachLevel = new int[maxLevel];
        xpForEachLevel[0] = 0;
        xpForEachLevel[1] = baseLevelXP;

        for (int i = 2; i < xpForEachLevel.Length; i++)
        {
            xpForEachLevel[i] = i * baseLevelXP;
        }
    }
}
