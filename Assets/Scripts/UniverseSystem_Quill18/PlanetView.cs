using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;
using TMPro;
using System;

public class PlanetView : MonoBehaviour
{
    public Planet Planet;

    public TextMeshProUGUI PlanetTypeUI, PlanetSizeUI, PlanetRichnessUI, PlanetNameUI, PlanetTraitUI;
    public GameObject colonyRowPrefab;
    public Transform colonyView;

    private void Start()
    {
        //Debug.Log("PlanetView::OnEnable -- " + Planet.PlanetName);

        PlanetNameUI.text = Planet.planetName;

        UpdateUIElements();
    }

    private void OnDisable()
    {

    }

    private void UpdateUIElements()
    {
        PlanetTypeUI.text = Enum.GetName(typeof(PlanetType), Planet.planetType);
        PlanetSizeUI.text = Enum.GetName(typeof(PlanetSize), Planet.planetSize);
        PlanetRichnessUI.text = Enum.GetName(typeof(PlanetRichness), Planet.planetRichness);
        UpdatePlanetView();
    }

    public void UpdatePlanetView()
    {
        foreach (Colony colony in Planet.colonies)
        {
            var obj = Instantiate(colonyRowPrefab, colonyView);
            obj.GetComponent<ColonyRow>().Colony = colony;
        }
    }
}
