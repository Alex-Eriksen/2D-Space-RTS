using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Starlight
{
    public static class GalaxyConfig
    {
        // Settings for making a galaxy, set by the player.
        public static int NumPlayers = 8;
        public static int NumStars = 160;

        // Total width/height of the range of star positions in Unity world units.
        public static int GalaxyWidth = 100;
    }

    public class Galaxy
    {
        public Galaxy()
        {
            m_starSystems = new List<StarSystem>();
            m_empires = new List<Empire>();
            m_planets = new List<Planet>();
            m_colonies = new List<Colony>();
        }

        private List<StarSystem> m_starSystems;
        private List<Planet> m_planets;
        private List<Colony> m_colonies;
        private List<Empire> m_empires;
        private int[] m_numOfStarClasses = { 0, 0, 0, 0, 0, 0, 0 };

        public void UpdateGalaxy()
        {
            foreach (Empire empire in m_empires)
            {
                empire.UpdateEmpire();
            }
        }

        public Empire GetEmpire( int index)
        {
            return m_empires[ index ];
        }

        public StarSystem GetStarSystem(int starSystemId)
        {
            return m_starSystems[ starSystemId ];
        }

        public int GetNumStarSystems()
        {
            return m_starSystems.Count;
        }

        public void Generate()
        {
            int galaxyWidth = GalaxyConfig.GalaxyWidth;

            for (int i = 0; i < GalaxyConfig.NumStars; i++)
            {
                StarSystem starSystem = new StarSystem();

                starSystem.Position = new Vector3(
                        Random.Range(-galaxyWidth / 2, galaxyWidth / 2),
                        Random.Range(-galaxyWidth / 2, galaxyWidth / 2),
                        0);

                int starType = Random.Range(-2, 3); // TODO: Make this better, and more complicated.

                StarClass starClass = GenerateStarClass();

                m_numOfStarClasses[ (int) starClass ]++;

                starSystem.Generate( starClass, m_numOfStarClasses[ (int) starClass ] );

                m_starSystems.Add(starSystem);
            }

            Debug.Log("Num Stars Generated: " + m_starSystems.Count);
            for (int i = 0; i < m_numOfStarClasses.Length; i++)
            {
                Debug.Log( $"Num Class {System.Enum.GetName( typeof( StarClass ), i )} Stars Generated: {m_numOfStarClasses[ i ]}" );
            }

            // If we go multiplayer we need a different way to do this.
            for (int i = 0; i < GalaxyConfig.NumPlayers; i++)
            {
                Empire empire;
                if(i == 0)
                {
                    // 0th player is the human.
                    empire = new Empire_Human(i);
                }
                else
                {
                    empire = new Empire_AI(i);
                }

                m_empires.Add(empire);
            }

            Debug.Log("Num Empires Generated: " + m_empires.Count);
        }

        public StarClass GenerateStarClass()
        {
            float[] weights = new float[7];
            weights[ 0 ] = Config.GetFloat( "STAR_CLASS_M_WEIGHT" );
            weights[ 1 ] = Config.GetFloat( "STAR_CLASS_K_WEIGHT" );
            weights[ 2 ] = Config.GetFloat( "STAR_CLASS_G_WEIGHT" );
            weights[ 3 ] = Config.GetFloat( "STAR_CLASS_F_WEIGHT" );
            weights[ 4 ] = Config.GetFloat( "STAR_CLASS_A_WEIGHT" );
            weights[ 5 ] = Config.GetFloat( "STAR_CLASS_B_WEIGHT" );
            weights[ 6 ] = Config.GetFloat( "STAR_CLASS_O_WEIGHT" );

            float totalWeights = weights.Sum();

            float r = Random.Range( 0, totalWeights );

            for (int i = 0; i < weights.Length; i++)
            {
                if(r < weights[i])
                {
                    return (StarClass) i;
                }
                r -= weights[i];
            }

            return StarClass.M;
        }

        public void Load()
        {

        }

        public void Save()
        {

        }
    }
}