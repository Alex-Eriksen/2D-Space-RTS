using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starlight;

public class UniverseManager : MonoBehaviour
{
    private static UniverseManager m_instance;
    public static UniverseManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UniverseManager>();
            }
            return m_instance;
        }
    }

    // This script is responsible for holding the main Galaxy data object,
    // triggering file save/loads or triggering the generation of a new galaxy.
    

    private Galaxy m_galaxy;

    public Empire_Human HumanEmpire { get; protected set; } // The player that the UI represents.

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (UITimeManager.IsPaused)
        {
            return;
        }

        m_galaxy.UpdateGalaxy();
    }

    public void Generate()
    {
        Debug.Log("UniverseManager::Generate -- Generating a new Galaxy");

        m_galaxy = new Galaxy();

        m_galaxy.Generate();

        // This is "fine" if we are a strictly single-player game,
        // but if we want to randomize the player slot or have many
        // local human player (i.e. Hotseat), then we'll
        // need a better way to cycle through them.
        HumanEmpire = (Empire_Human)m_galaxy.GetEmpire(0);

        // Generate visuals.
        ViewManager.Instance.GalaxyVisuals.InitiateVisuals(m_galaxy);
    }
}
