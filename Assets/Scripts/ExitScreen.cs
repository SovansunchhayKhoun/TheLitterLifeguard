using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScreen : MonoBehaviour
{
    public GameObject menu;
    private bool isOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
    void ToggleMenu()
    {
        isOpen = !isOpen;
        menu.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void Resume()
    {
        Debug.Log("Resume");
        isOpen = false;
        menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Exit()
    {

        SceneManager.LoadScene(SceneNameEnum.LEVEL_SCENE);
        // Time.timeScale = 1;
        // Application.Quit();
    }
}
