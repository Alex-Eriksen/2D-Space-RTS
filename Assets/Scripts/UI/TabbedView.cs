using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbedView : MonoBehaviour
{
    public List<GameObject> ContentObjects = new List<GameObject>();

    private void Start()
    {
        SwitchTab(0);
    }

    public void SwitchTab( int index )
    {
        foreach (GameObject gameObject in ContentObjects)
        {
            gameObject.SetActive(false);
        }
        ContentObjects[ index ].SetActive(true);
    }
}
