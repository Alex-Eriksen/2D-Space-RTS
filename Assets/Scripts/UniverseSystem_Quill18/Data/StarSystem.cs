using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starlight
{
    public class StarSystemGraphic
    {
        public StarSystemGraphic(StarSystem starSystem)
        {
            m_starSystem = starSystem;
        }

        private readonly StarSystem m_starSystem;

        public string CloseUpPath { get { return $"Stars/Prefabs/Star-CloseUp"; } }
        public string StarPath { get { return $"Stars/Prefabs/Star"; } }
    }

    public class StarSystem
    {
        public StarSystem()
        {
            m_planets = new Planet[ GetMaxPlanets() ];
        }

        public Vector3 Position;

        private Planet[] m_planets;

        public Star MainSequenceStar => m_mainSequenceStar;
        private Star m_mainSequenceStar;

        public StarSystemGraphic StarSystemGraphic;

        public string systemName;

        public Planet GetPlanet( int planetIndex )
        {
            return m_planets[ planetIndex ];
        }

        public void Generate( StarClass starClass, int numOfStarClass )
        {
            StarSystemGraphic = new StarSystemGraphic(this);

            GenerateMainSequenceStar( starClass, numOfStarClass );

            GeneratePlanets();
        }

        public int GetNumPlanets()
        {
            int c = 0;
            for (int i = 0; i < GetMaxPlanets(); i++)
            {
                if(m_planets[i] != null)
                {
                    c++;
                }
            }

            return c;
        }

        public int GetMaxPlanets()
        {
            return Config.GetInt("STAR_MAX_PLANETS");
        }

        public Planet GetPlanetAtIndex( int i )
        {
            return m_planets[ i ];
        }

        private void GeneratePlanets()
        {
            // StarType might influence the change to spawn a certain type of planet or even a planet at all.

            float planetChange = 0.50f;

            for (int i = 0; i < GetMaxPlanets(); i++)
            {
                if(UnityEngine.Random.Range(0f, 1f) <= planetChange)
                {
                    Planet planet = new Planet(i);
                    m_planets[ i ] = planet;
                    planet.planetName = GeneratePlanetName(i);

                    int size_max = (int)PlanetSize.COUNT;

                    planet.planetType = GeneratePlanetType(i);

                    planet.planetSize = 
                        (PlanetSize)Enum
                        .GetValues(typeof(PlanetSize))
                        .GetValue(UnityEngine.Random.Range(0, size_max));

                    planet.planetRichness = GeneratePlanetRichness(planet.planetType, i);

                    planet.PlanetGraphic = new PlanetGraphic(planet);

                    // TODO: Debugging purposes!! TEMPORARY!!
                    planet.colonies.Add(new Colony(0));
                    planet.colonies[ 0 ].Generate(planet);
                    //

                    planet.Generate();
                }
            }
        }

        private void GenerateMainSequenceStar(StarClass starClass, int numOfStarClass)
        {
            Star star = new Star();
            star.starClass = starClass;

            star.Generate( numOfStarClass );

            systemName = star.starName;
            m_mainSequenceStar = star;
        }

        private string GeneratePlanetName(int pos)
        {
            return systemName + " " + (pos + 1).ToRoman();
        }

        // TODO: I don't want it to generate with a random chance.
        private PlanetType GeneratePlanetType(int pos)
        {
            float goldilockRange = 0.5f;

            // TODO: Create weights for planet types.

            float asteroidWeight = 1.0f;

            float distance = (float)pos / (float) GetMaxPlanets();
            float distanceSquared = distance * distance;

            // The chance of a gas giant is higher the further away from the star the planet is.
            float gasGiantWeight = Mathf.Lerp(0.0f, 1.0f, distanceSquared);
            float lifelessWeight = Mathf.Lerp(0.0f, 2.0f, distance);
            float goldilocksWeight = Mathf.Lerp(1.0f, 0.0f, 2.0f * Mathf.Abs(goldilockRange - distance));

            float allWeights = gasGiantWeight + goldilocksWeight + asteroidWeight + lifelessWeight;

            // Select the number that determines the planet type.
            float r = UnityEngine.Random.Range(0.0f, allWeights);

            if(r < gasGiantWeight)
            {
                return PlanetType.GasGiant;
            }

            r -= gasGiantWeight;

            if(r < lifelessWeight)
            {
                return (PlanetType) Enum.GetValues(typeof( PlanetType ) ).GetValue(UnityEngine.Random.Range(5, 10));
            }

            r -= lifelessWeight;

            if(r < goldilocksWeight)
            {
                return PlanetType.Continental;
            }

            r -= goldilocksWeight;

            // If we get here it is because we rolled in the asteroid weight.
            return PlanetType.Asteroid;
        }

        // TODO: The planet's richness need to depend on the size of the planet,
        // the distance it is from the star and/or the type of star, also
        // the planet's type, asteroids should have less resources but more valuable.
        private PlanetRichness GeneratePlanetRichness( PlanetType planetType, int pos )
        {
            float distance = (float) pos / (float) GetMaxPlanets();

            if (planetType == PlanetType.Asteroid)
            {
                return (PlanetRichness) Enum.GetValues(typeof(PlanetRichness)).GetValue(UnityEngine.Random.Range(1, (int)PlanetRichness.COUNT));
            }

            if(planetType == PlanetType.GasGiant)
            {
                return (PlanetRichness) Enum.GetValues(typeof(PlanetRichness)).GetValue(UnityEngine.Random.Range(2, (int) PlanetRichness.COUNT));
            }

            return PlanetRichness.VeryPoor;
        }

        public int GetStarTypeIndex()
        {
            // Weird hacky function to convert from -2...+2 range to a 0...4 range

            return 1;
        }

        public void Load()
        {

        }

        public void Save()
        {

        }
    }
}