using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public RecipeType type;
    public RecipeRequirement[] requirements;
}

[System.Serializable]
public struct RecipeRequirement
{
    public ItemObject item;
    public int amount;
}

public enum RecipeType
{
    Ingot,
    Ration,
    Parts,
    Building,
    Ship
}
