using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCombiner : MonoBehaviour
{
    public Transform ModelGameObject;

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

            float massToAdd = colliderRigidbody.mass / 2f;
            Vector3 colliderModelScale = planetCombinerFromCollider.ModelGameObject.localScale;

            _rigidbody.mass += massToAdd;
            transform.localScale += colliderModelScale;

            soundPlayer.PlayClip();

            Destroy(other.gameObject);
        }
    }
}
