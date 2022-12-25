using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Animator crossfadeTransition;
    private bool isPaused;
    private MagicWand magicWand;
    private PlayerController playerController;

    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Player")) return;
        magicWand = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MagicWand>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    }

    private void Update()
    {
        EventManager.OnPlayCrossfade.AddListener(PlayCrossfade);
        EventManager.OnPlayerDeath.AddListener(ActivateDeathPanel);
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
        magicWand.IsPaused = true;
        playerController.IsPaused = true;
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
        magicWand.IsPaused = false;
        playerController.IsPaused = false;
    }

    public void ExitToMenu()
    {
        Destroy(pausePanel);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    private void ActivateDeathPanel()
    {
        Time.timeScale = 0;
        deathPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}