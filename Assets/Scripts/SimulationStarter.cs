using UnityEngine;
using TMPro;

public class SimulationStarter : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToToggle;
    [SerializeField] private GameObject _collisionParticleEffect;
    [SerializeField] private TMP_Text _pressEText;

    private bool _simulationRunning;

    private void Start()
    {
        _simulationRunning = false;
    }

    public bool IsSimulationRunning() => _simulationRunning;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!_simulationRunning)
            {
                StartSimulation();
            }
            else
            {
                StopSimulation();
            }
        }
    }

    private void StopSimulation()
    {
        _pressEText.text = "press E to start simulation";
        _simulationRunning = false;

        foreach (var planet in PlanetListHolder.Instance.PlanetsBuilt)
        {
            PlayDestroyEffects(planet);
        }
        foreach(var toggle in _objectsToToggle)
        {
            toggle.SetActive(true);
        }
    }

    private void PlayDestroyEffects(GameObject planet)
    {
        planet.GetComponentInChildren<ClipHolder>().PlayClip();
        Instantiate(_collisionParticleEffect, planet.transform.position, Quaternion.identity);

        var modelGameObject = planet.GetComponent<PlanetCombiner>().GetModelGameObject;

        modelGameObject.gameObject.SetActive(false);
        modelGameObject.GetComponentInParent<SphereCollider>().enabled = false;
        modelGameObject.GetComponentInParent<Rigidbody>().mass = 0;

        Destroy(planet, 2.1f);
    }

    private void StartSimulation()
    {
        _pressEText.text = "press E to stop simulation";
        _simulationRunning = true;

        foreach (var planet in PlanetListHolder.Instance.PlanetsBuilt)
        {
            planet.GetComponent<TrailRenderer>().enabled = true;
            planet.GetComponent<PlanetAttractor>().enabled = true;
        }

        foreach(var toggle in _objectsToToggle)
        {
            toggle.SetActive(false);
        }
    }
}
