using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;

[CreateAssetMenu(menuName = "Crafting/New Building Object")]
public class BuildingObject : ScriptableObject
{
    public int buildingID;
    public string buildingName;
    [TextArea(5, 10)]
    public string buildingDescription;
    public BuildingType buildingType;
    public Recipe buildingRecipe;
}
