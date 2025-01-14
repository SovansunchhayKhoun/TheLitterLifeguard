using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text pointCounter;
    public static int points = 0;
    public Text TimerText;
    // [SerializeField] TMP_Text timer;
    public static bool timerIsRunning = false;
    [SerializeField] GameObject[] tooltips;
    [SerializeField] TrashInteract trashManager;
    public bool canClick = false;
    public float uiDisableTime = 0.25f;
    public TextMeshProUGUI LeftOverTrashText;
    [SerializeField] bool isMenu = false;

    public GameObject GameOverPanel;
    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI ScoreText;

    void Awake()
    {
        if (isMenu)
            return;

        DisplayTooltip(4, true, 0f);
        GameOverPanel.SetActive(false); // Hide the Game Over panel initially
    }

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false); // Hide the Game Over panel initially
        if (isMenu)
            return;

        timerIsRunning = true;
        points = 0;
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMenu)
            return;

        UpdateLeftOverTrash();
        if (timerIsRunning)
        {
            if (GameplayManager.time > 0)
            {
                GameplayManager.time -= Time.deltaTime;
                // DisplayTime(time);
                UpdateTimerDisplay();
            }
            else
            {
                Debug.Log("Time's up!");
                GameplayManager.time = 0;
                timerIsRunning = false;
                SaveGameResults();
                ToggleGameOver();
            }
        }

        if (NewTrash.isGameOver)
        {
            SaveGameResults();
            ToggleGameOver();
        }
    }
    private void UpdateLeftOverTrash()
    {
        switch (LevelManager.selectedLevel)
        {
            case 1:
                LeftOverTrashText.text = (Level1Manager.NumTrash - points).ToString();
                break;
            case 2:
                LeftOverTrashText.text = (Level2Manager.NumTrash - points).ToString();
                break;
            case 3:
                LeftOverTrashText.text = (Level3Manager.NumTrash - points).ToString();
                break;
        }
    }
    private void UpdateTimerDisplay()
    {
        int minutes = (int)(GameplayManager.time / 60);
        int seconds = (int)(GameplayManager.time % 60);
        TimerText.text = $"{minutes}mn{seconds:D2}s";  // D2 formats seconds to always show 2 digits
    }

    public void ToggleGameOver()
    {
        ScoreText.text = UIManager.points.ToString();
        GameOverPanel.SetActive(true);

        StopSceneAndFocus();
        if (UIManager.points > 0)
        {
            ResultText.text = "You Win";
        }
        else
        {
            ResultText.text = "Game Over";
        }
    }

    void StopSceneAndFocus()
    {
        // Pause the game
        Time.timeScale = 0;

        // Disable other scene objects (optional)
        DisableSceneObjects();

        Cursor.lockState = CursorLockMode.None; // Locks cursor to the center of the screen
        Cursor.visible = true; // Hides the cursor
    }

    void DisableSceneObjects()
    {
        ToggleScriptState<PlayerController>(false);
        ToggleScriptState<CameraSwitch>(false);
        ToggleScriptState<FishingRodSfx>(false);

        // Disable animations
        var animators = FindObjectsOfType<Animator>();
        foreach (var animator in animators)
        {
            animator.enabled = false;
        }
    }
    private void ToggleScriptState<T>(bool enabled) where T : MonoBehaviour
    {
        var controller = FindObjectOfType<T>();
        if (controller != null)
        {
            controller.enabled = enabled;
        }
    }
    //procedure for tooltip when gotten wrong
    //procedure for intial tooltip that plays at the start of the game

    public void AddPoint()
    {
        points++;
        pointCounter.text = points.ToString();
    }

    public void RemovePoint()
    {
        if (points - 1 >= 0)
            points--;
        pointCounter.text = points.ToString();
    }

    public void ChangeClickBool(bool change)
    {
        canClick = change;
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    // private void DisplayTime(float timeToDisplay)
    // {
    //     timeToDisplay++;
    //     float minutes = Mathf.FloorToInt(timeToDisplay / 60);
    //     float seconds = Mathf.FloorToInt(timeToDisplay % 60);
    //     timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    // }

    public void UIRemoveTooltip(int type)
    {
        StartCoroutine(WaitForTooltip(type, false, 0f));
    }

    public void DisplayTooltip(int type, bool action, float waitTime)
    {
        //int tooltipToDisplay = type-1;
        //int type = trashManager.type;
        StartCoroutine(WaitForTooltip(type, action, waitTime));
    }

    private IEnumerator WaitForTooltip(int type, bool action, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tooltips[type - 1].SetActive(action);
        //Time.timeScale = action;
        if (action) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void MenuPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void SaveGameResults()
    {
        // Navigate to the root directory by going up from the 'Assets' folder
        string path = Directory.GetParent(Application.dataPath).FullName + "/GameResults.txt";
        string content = "Game Over!\n";
        content += "Points: " + points + "\n";
        content += "Time Remaining: " + Mathf.FloorToInt(GameplayManager.time) + " seconds\n";
        content += "Date: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n";

        File.WriteAllText(path, content);
        Debug.Log("Game results saved to: " + path);
    }
}
