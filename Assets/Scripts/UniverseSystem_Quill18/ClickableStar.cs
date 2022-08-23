using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Starlight;
using TMPro;

public class ClickableStar : MonoBehaviour
{
    public StarSystem starSystem;

    private void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = starSystem.systemName;

        GetComponentInChildren<SpriteRenderer>().color = starSystem.MainSequenceStar.Color;
    }

    public void OnClick()
    {
        Debug.Log("Open System View - " + starSystem.systemName);
        var obj = Instantiate(Resources.Load<GameObject>("UIPrefabs/View/SystemView"));
        obj.GetComponent<SystemView>().starSystem = starSystem;
        ViewManager.Instance.Open(obj, starSystem.systemName);
    }
}
