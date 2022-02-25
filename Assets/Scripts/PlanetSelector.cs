using UnityEngine;

public class PlanetSelector : MonoBehaviour
{
    [SerializeField] private Transform _planetSpawnPoint;
    [SerializeField] private PlanetInfoUI _planetInfoUI;
    private GameObject _currentSelectedPlanet;

    private void Start()
    {
        _currentSelectedPlanet = PlanetListHolder.Instance.AllPlanetsAvailable[0];
    }

    private void Update()
    {
        SelectPlanetByKey();
    }

    public GameObject GetCurrentSelectedPlanet() => _currentSelectedPlanet;

    private void SelectPlanetByKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateSelectedPlanet(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateSelectedPlanet(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateSelectedPlanet(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateSelectedPlanet(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateSelectedPlanet(4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            UpdateSelectedPlanet(5);
        }
    }

    private void UpdateSelectedPlanet(int planetIndex)
    {
        _currentSelectedPlanet = PlanetListHolder.Instance.AllPlanetsAvailable[planetIndex];
        _planetInfoUI.UpdatePlanetInfoBox(_currentSelectedPlanet.GetComponent<PlanetData>());
        _planetSpawnPoint.localPosition = new Vector3(0, 0, _currentSelectedPlanet.GetComponent<PlanetData>().SpawnPointDistance);
    }
}
