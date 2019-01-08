using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 0;
    int health = 0;
    [SerializeField] int scoreToReachBoss = 5000;
    bool bossActive = false;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if(!bossActive && score >= scoreToReachBoss)
        {
            bossActive = true;
            FindObjectOfType<Level>().SwitchToBoss();
        }
    }

    public void BossDefeated()
    {
        if (bossActive)
        {
            scoreToReachBoss += score;
            bossActive = false;
        }
        FindObjectOfType<Level>().SwitchBack();
    }

    public int GetHealth()
    {
        return health;
    }

    public void UpdateHealth(int health)
    {
        this.health = health;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
