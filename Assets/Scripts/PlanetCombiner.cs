using System.Collections;
using System.Collections.Generic;
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

        if(_rigidbody.mass > colliderRigidbody.mass)
        {
            var planetCombinerFromCollider = colliderRigidbody.GetComponent<PlanetCombiner>();
            var soundPlayer = GetComponentInChildren<ClipHolder>();
            var soundPlayerFromCollider = other.gameObject.GetComponentInChildren<ClipHolder>();

            float massToAdd = colliderRigidbody.mass;
            Vector3 colliderModelScale = planetCombinerFromCollider._modelGameObject.localScale;

            _rigidbody.mass += massToAdd;
            transform.localScale += colliderModelScale;

            soundPlayer.PlayClip();
            Instantiate(_collisionParticleEffect, other.contacts[0].point, Quaternion.identity);

            Destroy(other.gameObject);
        }
    }
}
