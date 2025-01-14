using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTrash : MonoBehaviour
{

    [SerializeField] GameObject[] recycle;
    [SerializeField] GameObject[] compost;
    [SerializeField] GameObject[] landfill;

    public int trashType;
    [SerializeField] Transform spawnPoint;
    public GameObject trash;
    public static bool isGameOver;

    public GameObject[] bins;
    public string[] anims;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MakeNewTrash();

        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.selectedLevel == 1 && (UIManager.points >= Level1Manager.NumTrash || !UIManager.timerIsRunning))
        {
            isGameOver = true;
        }
        if (LevelManager.selectedLevel == 2 && (UIManager.points >= Level2Manager.NumTrash || !UIManager.timerIsRunning))
        {
            isGameOver = true;
        }
        if (LevelManager.selectedLevel == 3 && (UIManager.points >= Level3Manager.NumTrash || !UIManager.timerIsRunning))
        {
            isGameOver = true;
        }
    }


    public void MakeNewTrash()
    {
        trashType = Random.Range(0, 3) + 1; // 1-3
        Destroy(trash);
        if (trashType == 1)
        {
            //recycle
            int variant = Random.Range(0, recycle.Length);
            trash = Instantiate(recycle[variant], spawnPoint.position, Quaternion.identity, spawnPoint);
        }
        if (trashType == 2)
        {
            //compost
            int variant = Random.Range(0, compost.Length);
            trash = Instantiate(compost[variant], spawnPoint.position, Quaternion.identity, spawnPoint);
        }
        if (trashType == 3)
        {
            //landfill
            int variant = Random.Range(0, landfill.Length);
            trash = Instantiate(landfill[variant], spawnPoint.position, Quaternion.identity, spawnPoint);
        }
        Camera mainCam = Camera.main;
        mainCam.gameObject.GetComponent<PhysicsPickup>().objectToMove = trash.transform;
    }
}