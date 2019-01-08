using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [SerializeField] float musicDelay =2.1f;
    [SerializeField] AudioSource bossMusic;
    [SerializeField] AudioSource victoryMusic;
    bool musicActive = false;
    float timer = 0f;
    Enemy enemyBoss;

    private void Start()
    {
        enemyBoss = FindObjectOfType<Enemy>();
    }

    // Use this for initialization
    void Update ()
    {
        if (timer < musicDelay)
            timer += Time.deltaTime;
        else if(!musicActive)
            MusicHandler();
        if(enemyBoss.health <= 0)
        {
            DeathHandler();
        }
	}

    private void DeathHandler()
    {
        FindObjectOfType<GameSession>().BossDefeated();
    }

    private void MusicHandler()
    {
        musicActive = true;
        bossMusic.Play();
    }
}
