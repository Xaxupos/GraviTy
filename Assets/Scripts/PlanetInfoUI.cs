using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetInfoUI : MonoBehaviour
{
    [SerializeField] private Image _planetImage;
    [SerializeField] private TMP_Text _planetName;
    [SerializeField] private TMP_Text _planetMass;
    [SerializeField] private TMP_Text _planetRotation;
    [SerializeField] private TMP_Text _planetStatic;

    public void UpdatePlanetInfoBox(PlanetData planetData)
    {
        _planetImage.sprite = planetData.PlanetSprite;
        _planetName.text = planetData.PlanetName;
        _planetMass.text = "Mass: " + planetData.PlanetMass;
        _planetRotation.text = "Rotation: " + planetData.PlanetRotation;
        _planetStatic.text = "Static: " + planetData.PlanetStatic;
    }
}
