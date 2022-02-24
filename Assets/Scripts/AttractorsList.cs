using System.Collections.Generic;
using UnityEngine;

public class AttractorsList : MonoBehaviour
{
    public static AttractorsList Instance { get; private set; }
    public List<PlanetAttractor> PlanetAttractors = new List<PlanetAttractor>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void AddPlanetToList(PlanetAttractor planet)
    {
        PlanetAttractors.Add(planet);
    }

    public void RemovePlanetFromList(PlanetAttractor planet)
    {
        PlanetAttractors.Remove(planet);
    }
}
