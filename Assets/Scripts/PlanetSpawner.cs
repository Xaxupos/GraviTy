using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private SimulationStarter _simulationStarter;
    [SerializeField] private PlanetSelector _planetSelector;
    [SerializeField] private Transform _planetSpawnPoint;

    void Update()
    {
        if(_simulationStarter.IsSimulationRunning()) return;

        if(_planetSelector.GetCurrentSelectedPlanet() != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                var planet = Instantiate(_planetSelector.GetCurrentSelectedPlanet(), _planetSpawnPoint.position, Quaternion.identity);
                PlanetListHolder.Instance.AddPlanetBuiltToList(planet);
            }
        }
    }
}
