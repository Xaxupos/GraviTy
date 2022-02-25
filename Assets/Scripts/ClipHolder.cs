using UnityEngine;

public class ClipHolder : MonoBehaviour
{
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip _clip;

    public void PlayClip()
    {
        _AudioSource.clip = _clip;
        _AudioSource.pitch = Random.Range(0.5f, 1.5f);
        _AudioSource.Play();
    }
}
