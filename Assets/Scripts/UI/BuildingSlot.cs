using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Starlight;

public class BuildingSlot : MonoBehaviour
{
    public Building Building;

    public Image Icon;

    private void Start()
    {
        Icon.sprite = Resources.Load<Sprite>(Building.BuildingGraphics.IconPath);
    }
}
