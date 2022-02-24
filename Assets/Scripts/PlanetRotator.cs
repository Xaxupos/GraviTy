using UnityEngine;

public class PlanetRotator : MonoBehaviour
{
    [SerializeField] private float _rotateForce = 5f;

    void Update()
    {
        transform.Rotate(0, _rotateForce * Time.deltaTime, 0);
    }
}
