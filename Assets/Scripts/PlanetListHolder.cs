using System.Collections.Generic;
using UnityEngine;

public class PlanetListHolder : MonoBehaviour
{
    public static PlanetListHolder Instance { get; private set; }
    public List<GameObject> AllPlanetsAvailable = new List<GameObject>();
    public List<PlanetAttractor> PlanetAttractors = new List<PlanetAttractor>();
    public List<GameObject> PlanetsBuilt = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void AddPlanetAttractorToList(PlanetAttractor planet)
    {
        PlanetAttractors.Add(planet);
    }

    public void RemovePlanetAttractorFromList(PlanetAttractor planet)
    {
        PlanetAttractors.Remove(planet);
    }

    public void AddPlanetBuiltToList(GameObject planet)
    {
        PlanetsBuilt.Add(planet);
    }

    public void RemovePlanetBuiltFromList(GameObject planet)
    {
        PlanetsBuilt.Remove(planet);
    }
}
