﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{

    private GameObject menu;
    private GameObject pauseMenu;
    private GameObject optionMenu;

    public GameObject firstPauseMenu, firstOption;

    private SoundManager soundManager;
 

    /* Time scale from before the game was paused.  This is necessary so that if
     * the game is resumed while time warp is enabled, the time warp will continue.
     */
    private float originalTimeScale;


    // Start is called before the first frame update

    private void Awake()
    {
        menu = transform.GetChild(0).gameObject;
        pauseMenu = transform.GetChild(0).GetChild(0).gameObject;
        optionMenu = transform.GetChild(0).GetChild(1).gameObject;
        soundManager = FindObjectOfType<SoundManager>();
    }
    void Start()
    {   

        // Set a default value for originalTimeScale.
        originalTimeScale = Time.timeScale;

        // call menu once 

    }

    public void PressPause()
    {
        if (menu && !menu.activeInHierarchy)
        {
            PauseGame();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstPauseMenu);
        }
        else
        {   

            if (optionMenu != null && optionMenu.activeInHierarchy)
            {
                optionMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else
            {
                ResumeGame();
            }
        }

    }

    public bool CheckPaused()
    {
        return menu.activeInHierarchy;
    }

    public void PauseGame()
    {
        soundManager.SetLowPassFilterEnabled(true);
        menu.SetActive(true);
        pauseMenu.SetActive(true);
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;


    }

    public void ResumeGame()
    {   
        soundManager.SetLowPassFilterEnabled(false);
        if (pauseMenu != null & pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
        }
        menu.SetActive(false);
        // Delay();
        Time.timeScale = originalTimeScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void GameOption()
    {
        pauseMenu.SetActive(false);
    }

    public void OptionButtonNavi()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOption);
    }

    public void BackButtonNavi()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstPauseMenu);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
    }


}
