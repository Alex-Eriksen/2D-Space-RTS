using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;
using TMPro;

public class ColonyRow : MonoBehaviour
{
    public Colony Colony;

    public TextMeshProUGUI colonyNameUI, creditFlowUI, resourceFlowUI, populationUI;

    public void Update()
    {
        if (Colony == null)
        {
            return;
        }

        colonyNameUI.text = Colony.ColonyName;
        populationUI.text = Colony.Population + "M";
        resourceFlowUI.text = Colony.ResourceFlow + "K";
        creditFlowUI.text = Colony.CreditFlow + "K";
    }

    public void OnClick()
    {
        Debug.Log( "ColonyRow::OnClick -- " + Colony.ColonyName );
        var obj = Instantiate( Resources.Load<GameObject>( "UIPrefabs/Colony/ColonyView" ) );
        obj.GetComponent<ColonyView>().Colony = Colony;
        ViewManager.Instance.Open( obj, Colony.ColonyName);
    }
}
