using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2.1f;
    [SerializeField] AudioClip startSound;
    int sceneToLoad;
    bool isPlaying = false;
    public void LoadGameScene()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(WaitAndLoad(1));
        if (FindObjectOfType<GameSession>())
            FindObjectOfType<GameSession>().ResetGame();
        IntroSound();
    }

    private void IntroSound()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            AudioSource.PlayClipAtPoint(startSound, Camera.main.transform.position);
        }
    }

    public void SwitchToBoss()
    {
        StartCoroutine(WaitAndLoad(2));
        //IntroSound();
    }

    public void SwitchBack()
    {
        StartCoroutine(WaitAndLoad(1));
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(3));
    }

    IEnumerator WaitAndLoad(int sceneNumber)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneNumber);
        isPlaying = false;
    }

    public void LoadStartMenu()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
