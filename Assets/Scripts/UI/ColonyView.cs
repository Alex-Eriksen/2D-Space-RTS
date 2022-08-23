using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;

public class ColonyView : MonoBehaviour
{
    public Colony Colony;

    public Transform BuildingSlotContainerTransform;
    public GameObject BuildingSlotPrefab;

    private void Start()
    {
        for (int i = 0; i < Colony.BuiltBuildings.Count; i++)
        {
            var obj = Instantiate(BuildingSlotPrefab, BuildingSlotContainerTransform);
            var slot = obj.GetComponent<BuildingSlot>();
            slot.Building = Colony.BuiltBuildings[ i ];
        }
    }
}
