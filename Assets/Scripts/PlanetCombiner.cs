using UnityEngine;

public class PlanetCombiner : MonoBehaviour
{
    [SerializeField] private Transform _modelGameObject;
    [SerializeField] private GameObject _collisionParticleEffect;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public Transform GetModelGameObject => _modelGameObject;

    private void OnCollisionEnter(Collision other)
    {
        var colliderRigidbody = other.gameObject.GetComponent<Rigidbody>();
        var planetCombinerFromCollider = colliderRigidbody.GetComponent<PlanetCombiner>();
        var soundPlayer = GetComponentInChildren<ClipHolder>();

        if(_rigidbody.mass > colliderRigidbody.mass)
        {
            float massToAdd = colliderRigidbody.mass;
            Vector3 colliderModelScale = planetCombinerFromCollider._modelGameObject.localScale;

            AddMassAndScale(massToAdd, colliderModelScale);
            PlayCollisionEffects(other, soundPlayer);
            Destroy(other.gameObject);
        }

        if (_rigidbody.mass == colliderRigidbody.mass)
        {
            PlayCollisionEffects(other, soundPlayer);
            DisableModelAndCollider(planetCombinerFromCollider);
            Destroy(gameObject, 2.1f);
        }
    }

    private void DisableModelAndCollider(PlanetCombiner planetCombinerFromCollider)
    {
        _modelGameObject.gameObject.SetActive(false);
        _modelGameObject.GetComponentInParent<SphereCollider>().enabled = false;
        _modelGameObject.GetComponentInParent<Rigidbody>().mass = 0;

        planetCombinerFromCollider._modelGameObject.gameObject.SetActive(false);
        planetCombinerFromCollider._modelGameObject.GetComponentInParent<SphereCollider>().enabled = false;
        planetCombinerFromCollider._modelGameObject.GetComponentInParent<Rigidbody>().mass = 0;
    }

    private void AddMassAndScale(float massToAdd, Vector3 colliderModelScale)
    {
        _rigidbody.mass += massToAdd;
        transform.localScale += colliderModelScale;
    }

    private void PlayCollisionEffects(Collision other, ClipHolder soundPlayer)
    {
        soundPlayer.PlayClip();
        Instantiate(_collisionParticleEffect, other.contacts[0].point, Quaternion.identity);
    }
}
