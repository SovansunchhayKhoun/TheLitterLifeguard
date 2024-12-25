using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicate music
        }
        else
        {
            instance = this;
             DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
    }
}
