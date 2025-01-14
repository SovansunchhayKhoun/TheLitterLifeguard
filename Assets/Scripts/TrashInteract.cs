using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInteract : MonoBehaviour
{
    public int type;
    [SerializeField] NewTrash trashManager;
    [SerializeField] SoundManager sM;
    [SerializeField] UIManager uIM;
    public int clickCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        clickCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckClickCount();
    }

    void OnTriggerEnter(Collider collision)
    {
        transform.root.GetComponent<TrashBin>().canClose = true;
        sM.PlayTrashSound(collision.name);
        Debug.Log(type + "trigger type");
        Debug.Log(trashManager.trashType + "trash type");
        clickCounter += 1;
        if (trashManager.trashType == type)
        {
            //correct
            Debug.Log("Correct choice");
            sM.PlayPointSound(true);
            uIM.AddPoint();
        }
        else
        {
            //wrong
            Debug.Log("Wrong choice");
            sM.PlayPointSound(false);
            // uIM.RemovePoint();
            uIM.DisplayTooltip(type, true, uIM.uiDisableTime);
            uIM.canClick = false;
        }
        trashManager.MakeNewTrash(); //this will be used for making new trash
    }

    void CheckClickCount()
    {
        if (LevelManager.selectedLevel == 1 && clickCounter >= Level1Manager.NumTrash)
        {
            NewTrash.isGameOver = true;
        }
        if (LevelManager.selectedLevel == 2 && clickCounter >= Level2Manager.NumTrash)
        {
            NewTrash.isGameOver = true;
        }
        if (LevelManager.selectedLevel == 3 && clickCounter >= Level3Manager.NumTrash)
        {
            NewTrash.isGameOver = true;
        }
    }
}
