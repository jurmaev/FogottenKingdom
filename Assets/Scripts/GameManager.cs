using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Animator crossfadeTransition;
    private bool isPaused;

    private void Update()
    {
        EventManager.OnPlayCrossfade.AddListener(PlayCrossfade);
        if (pausePanel != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) PauseGame();
            else ContinueGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    private void PlayCrossfade()
    {
        crossfadeTransition.SetTrigger("start");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void ExitToMenu()
    {
        Destroy(pausePanel);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}