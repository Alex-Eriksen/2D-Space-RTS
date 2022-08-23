using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;

public class BuildingRecipe : ScriptableObject
{
    public BuildingObject product;
    public RecipeRequirement[] requirements;
}
