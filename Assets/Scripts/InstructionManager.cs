using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    public GameObject[] panels; // Assign instruction panels in order
    public GameObject gameUI; // Assign the gameplay UI
    private int currentPanelIndex = 0;

    void Start()
    {
        ShowPanel(0); // Show the first panel at the start
        Time.timeScale = 0;
    }

    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }
        currentPanelIndex = index;

        // Enable cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            ShowPanel(currentPanelIndex + 1);
        }
    }

    public void PreviousPanel()
    {
        if (currentPanelIndex > 0)
        {
            ShowPanel(currentPanelIndex - 1);
        }
    }

    public void StartGame()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        gameUI.SetActive(true); // Activate gameplay UI
        Time.timeScale = 1; // Resume game time

        // Disable cursor for gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowInstructions()
    {
        Time.timeScale = 0; // Pause the game
        gameUI.SetActive(false);
        ShowPanel(0); // Show the first instruction panel
    }

    private void Update()
    {
        // Check for F5 key to reopen instructions
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ShowInstructions();
        }
    }
}
