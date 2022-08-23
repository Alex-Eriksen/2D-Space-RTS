using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;
using UnityEngine.InputSystem;

public class GalaxyVisuals : MonoBehaviour
{
    private Galaxy m_galaxy;

    public void InitiateVisuals( Galaxy galaxy )
    {
        m_galaxy = galaxy;

        for (int i = 0; i < galaxy.GetNumStarSystems(); i++)
        {
            StarSystem starSystem = galaxy.GetStarSystem(i);

            GameObject go = Instantiate(
                Resources.Load<GameObject>(starSystem.StarSystemGraphic.StarPath), 
                starSystem.Position,
                Quaternion.identity,
                transform);

            go.GetComponent<ClickableStar>().starSystem = starSystem;
        }
    }
}
