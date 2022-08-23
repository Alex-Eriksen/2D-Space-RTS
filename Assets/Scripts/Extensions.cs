using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Extensions
{
    private static readonly Dictionary<int, string> m_romanValues = new Dictionary<int, string>()
    { 
        { 1, "I" }, { 4, "IV" }, { 5, "V" }, { 9, "IX" }, { 10, "X" }, { 40, "XL" }, 
        { 50, "L" }, { 90, "XC" }, { 100, "C" }, { 400, "CD" }, { 500, "D" }, { 900, "CM" }, { 1000, "M" }
    };

    public static void SetLayerRecursively(this Transform parent, int layer, int exclusionLayer = -1)
    {
        if(exclusionLayer == -1)
        {
            parent.gameObject.layer = layer;

            for (int i = 0, count = parent.childCount; i < count; i++)
            {
                parent.GetChild( i ).SetLayerRecursively( layer );
            }
        }
        else
        {
            if(parent.gameObject.layer != exclusionLayer)
            {
                parent.gameObject.layer = layer;
            }

            for (int i = 0, count = parent.childCount; i < count; i++)
            {
                parent.GetChild( i ).SetLayerRecursively( layer, exclusionLayer );
            }
        }
    }

    public static string ToRoman( this int number )
    {
        string romanResult = "";
        foreach (var item in m_romanValues.Reverse())
        {
            if (number <= 0)
                break;
            while (number >= item.Key)
            {
                romanResult += item.Value;
                number -= item.Key;
            }
        }
        return romanResult;
    }

}
namespace Starlight
{
    public static class StarlightUtils
    {
        public static float CalculateApparentSize( float actualSize, float distance)
        {
            return 2 * Mathf.Atan( actualSize / (2 * distance) ) * Mathf.Rad2Deg;
        }
    }

    public delegate void SmartAction<T>( SmartAction<T> self, T obj );
}
