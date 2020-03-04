﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int m_currentScore;
    private void Start()
    {
        Instance = this;
    }

    /// <summary>
    /// Changes the score
    /// </summary>
    /// <param name="p_addToScore"></param>
    public void ChangeScore(int p_addToScore)
    {
        m_currentScore += p_addToScore;
        GameObject.Find("Health").GetComponent<ValueHandler>().ValueSet(m_currentScore);
    }

}
