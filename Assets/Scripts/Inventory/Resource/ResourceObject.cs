using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/New Resource Object")]
public class ResourceObject : ItemObject
{
    public float resourceDensity;
    public ResourceType resourceType;
}
