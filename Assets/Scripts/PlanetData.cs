using UnityEngine;

public class PlanetData : MonoBehaviour
{
    public Sprite PlanetSprite;
    public string PlanetName;
    public float SpawnPointDistance;
    public float PlanetMass;
    public float PlanetRotation;
    public bool PlanetStatic;

    private void OnDestroy()
    {
        PlanetListHolder.Instance.RemovePlanetBuiltFromList(this.gameObject);
    }
}
