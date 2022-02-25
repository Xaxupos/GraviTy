using UnityEngine;

public class ClipHolder : MonoBehaviour
{
    public AudioSource AudioSource;

    [SerializeField] private AudioClip _clip;

    public void PlayClip()
    {
        AudioSource.clip = _clip;
        AudioSource.pitch = Random.Range(0.7f, 1.3f);
        AudioSource.Play();
    }
}
