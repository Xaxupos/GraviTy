using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class PlanetAttractor : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private const float G = 667.4f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        AttractorsList.Instance.AddPlanetToList(this);
    }

    private void FixedUpdate()
    {
        foreach(PlanetAttractor planet in AttractorsList.Instance.PlanetAttractors)
        {
            if(planet != this)
            {
                Attract(planet);
            }
        }
    }

    private void OnDisable()
    {
        AttractorsList.Instance.RemovePlanetFromList(this);
    }

    public Rigidbody GetRigidbody() => _rigidbody;

    private void Attract(PlanetAttractor planetToAttract)
    {
        Rigidbody rbToAttract = planetToAttract.GetRigidbody();
        Vector3 direction = _rigidbody.position - rbToAttract.position;
        
        float distanceBetweenObjects = direction.magnitude;

        if(distanceBetweenObjects == 0f) return;

        float forceMagnitude = G * (_rigidbody.mass * rbToAttract.mass) / Mathf.Pow(distanceBetweenObjects,2);

        Vector3 force = direction.normalized * forceMagnitude;
        rbToAttract.AddForce(force);
    }
}
