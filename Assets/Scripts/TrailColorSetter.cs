using UnityEngine;

public class TrailColorSetter : MonoBehaviour
{

    [SerializeField] private Gradient _gradient;
    [SerializeField] private TrailRenderer _trailRenderer;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _trailRenderer.colorGradient = _gradient;
    }

    private void Update()
    {
        _trailRenderer.time = _rigidbody.mass / 8.5f;
    }

}
