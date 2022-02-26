using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}
