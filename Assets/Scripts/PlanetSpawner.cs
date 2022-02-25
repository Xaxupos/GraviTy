using UnityEngine;
using TMPro;

public class PlanetSpawner : MonoBehaviour
{
    [field: SerializeField] public bool CustomSimulation { get; set; }
    [SerializeField] private int _planetsSpawnRange;
    [SerializeField] private int _planetsToSpawn;
    [SerializeField] private TMP_Text _planetsSpawnRangeText;
    [SerializeField] private TMP_Text _planetsCounter;
    [SerializeField] private SimulationStarter _simulationStarter;
    [SerializeField] private PlanetSelector _planetSelector;
    [SerializeField] private Transform _planetSpawnPoint;

    private void Start()
    {
        _planetsSpawnRange = 100;
        _planetsToSpawn = 0;
    }

    void Update()
    {
        if(_simulationStarter.IsSimulationRunning()) return;

        if(!CustomSimulation)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _planetsToSpawn--;
                if(_planetsToSpawn < 0) _planetsToSpawn = 0;
                _planetsCounter.text = "Planets: " + _planetsToSpawn;
                GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                _planetsToSpawn++;
                _planetsCounter.text = "Planets: " + _planetsToSpawn;
                GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                _planetsSpawnRange += 100;
                _planetsSpawnRangeText.text = "Spawn range: " + _planetsSpawnRange;
                GetComponent<AudioSource>().Play();
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                _planetsSpawnRange -= 100;
                if(_planetsSpawnRange < 100) _planetsSpawnRange = 100;
                _planetsSpawnRangeText.text = "Spawn range: " + _planetsSpawnRange;
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
        for (int i = 0; i < _planetsToSpawn; i++)
        {
            var xPosition = Random.Range(-_planetsSpawnRange, _planetsSpawnRange);
            var yPosition = Random.Range(-_planetsSpawnRange, _planetsSpawnRange);
            var zPosition = Random.Range(-_planetsSpawnRange, _planetsSpawnRange);

            Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);
            var planetToSpawnIndex = Random.Range(0, PlanetListHolder.Instance.AllPlanetsAvailable.Count);
            var planetToSpawn = PlanetListHolder.Instance.AllPlanetsAvailable[planetToSpawnIndex];
            var planet = Instantiate(planetToSpawn, spawnPosition, Quaternion.identity);
            PlanetListHolder.Instance.AddPlanetBuiltToList(planet);
        }
    }
}
