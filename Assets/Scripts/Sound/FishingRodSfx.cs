using UnityEngine;

public class FishingRodSfx : MonoBehaviour
{
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip successFishCastClip;
  [SerializeField] private AudioClip missFishCastClip;


  public AudioClip GetSuccessFishCastClip()
  {
    return successFishCastClip;
  }
  public AudioClip GetMissFishCastClip()
  {
    return missFishCastClip;
  }
  public void PlaySuccessFishCast()
  {
    audioSource.clip = successFishCastClip;
    audioSource.Play();
  }

  public void PlayMissFishCast()
  {
    audioSource.clip = missFishCastClip;
    audioSource.Play();
  }
}
