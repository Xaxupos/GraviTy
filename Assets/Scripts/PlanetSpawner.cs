using UnityEngine;
using TMPro;

public class PlanetSpawner : MonoBehaviour
{
    [field: SerializeField] public int PlanetsToSpawn { get; set; }
    [field: SerializeField] public bool CustomSimulation { get; set; }
    [SerializeField] private TMP_Text _planetsCounter;
    [SerializeField] private SimulationStarter _simulationStarter;
    [SerializeField] private PlanetSelector _planetSelector;
    [SerializeField] private Transform _planetSpawnPoint;

    private void Start()
    {
        PlanetsToSpawn = 0;
    }

    void Update()
    {
        if(_simulationStarter.IsSimulationRunning()) return;

        if(!CustomSimulation)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlanetsToSpawn--;
                if(PlanetsToSpawn < 0) PlanetsToSpawn = 0;
                _planetsCounter.text = "Planets to spawn: " + PlanetsToSpawn;
                GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlanetsToSpawn++;
                _planetsCounter.text = "Planets to spawn: " + PlanetsToSpawn;
                GetComponent<AudioSource>().Play();
            }
        }

        if(CustomSimulation &&  _planetSelector.GetCurrentSelectedPlanet() != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                var planet = Instantiate(_planetSelector.GetCurrentSelectedPlanet(), _planetSpawnPoint.position, Quaternion.identity);
                GetComponent<AudioSource>().Play();
                PlanetListHolder.Instance.AddPlanetBuiltToList(planet);
            }
        }
    }

    public void SpawnPlanets()
    {
        float valueToSet = PlanetsToSpawn * 60;

        for (int i = 0; i < PlanetsToSpawn; i++)
        {
            var xPosition = Random.Range(-valueToSet, valueToSet);
            var yPosition = Random.Range(-valueToSet, valueToSet);
            var zPosition = Random.Range(-valueToSet, valueToSet);

            Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);
            var planetToSpawnIndex = Random.Range(0, PlanetListHolder.Instance.AllPlanetsAvailable.Count);
            var planetToSpawn = PlanetListHolder.Instance.AllPlanetsAvailable[planetToSpawnIndex];
            var planet = Instantiate(planetToSpawn, spawnPosition, Quaternion.identity);
            PlanetListHolder.Instance.AddPlanetBuiltToList(planet);
        }
    }
}
