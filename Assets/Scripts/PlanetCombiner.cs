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

    private void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.CompareTag("Planet")) return;

        var colliderRigidbody = other.gameObject.GetComponent<Rigidbody>();
        var planetCombinerFromCollider = colliderRigidbody.GetComponent<PlanetCombiner>();
        var soundPlayer = GetComponentInChildren<ClipHolder>();

        if(_rigidbody.mass > colliderRigidbody.mass)
        {
            float massToAdd = colliderRigidbody.mass;
            Vector3 colliderModelScale = planetCombinerFromCollider._modelGameObject.localScale;

            _rigidbody.mass += massToAdd;
            transform.localScale += colliderModelScale;

            soundPlayer.PlayClip();
            Instantiate(_collisionParticleEffect, other.contacts[0].point, Quaternion.identity);

            Destroy(other.gameObject);
        }
        if(_rigidbody.mass == colliderRigidbody.mass)
        {
            soundPlayer.PlayClip();
            Instantiate(_collisionParticleEffect, other.contacts[0].point, Quaternion.identity);

            _modelGameObject.gameObject.SetActive(false);
            _modelGameObject.GetComponentInParent<SphereCollider>().enabled = false;
            _modelGameObject.GetComponentInParent<Rigidbody>().mass = 0;

            planetCombinerFromCollider._modelGameObject.gameObject.SetActive(false);
            planetCombinerFromCollider._modelGameObject.GetComponentInParent<SphereCollider>().enabled = false;
            planetCombinerFromCollider._modelGameObject.GetComponentInParent<Rigidbody>().mass = 0;

            Destroy(gameObject, 2.1f);
        }
    }
}
