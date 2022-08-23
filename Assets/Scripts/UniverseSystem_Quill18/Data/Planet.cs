using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starlight
{
    public class PlanetGraphic
    {
        public PlanetGraphic(Planet planet)
        {
            m_planet = planet;
        }

        private readonly Planet m_planet;

        public string PlanetPath { get { return $"Planets/Prefabs/Planet"; } }
    }

    // TODO: Figure out what planet types I want.
    public enum PlanetType { Asteroid, Barren, GasGiant, Toxic, Ocean, Arid, Desert, Tundra, Continental, Forest, Lava, COUNT }
    
    // TODO: PlanetSize, I don't want it to be a static enum, I want it to be a float.
    public enum PlanetSize { Tiny, Small, Medium, Large, Huge, COUNT }

    // TODO: PlanetRichness, I don't want it to be a static enum, I want it to be a percentage.
    public enum PlanetRichness { VeryPoor, Poor, Abundant, Rich, VeryRich, COUNT }

    public class Planet
    {
        public Planet( int planetIndex )
        {
            this.planetIndex = planetIndex;
            colonies = new List<Colony>();
        }

        private static readonly float m_radiusMin = 0.383f;
        private static readonly float m_radiusMax = 11.21f;

        private static readonly float m_massMin = 0.0553f;
        private static readonly float m_massMax = 317.8f;

        private static readonly Color[] m_colors =
        {
            new Color( 145f / 255f, 145f / 255f, 145f / 255f, 255f / 255f ), // Asteroid
            new Color( 168f / 255f, 168f / 255f, 0f / 255f, 255f / 255f ),   // Barren
            new Color( 255f / 255f, 0f / 255f, 212f / 255f , 255f / 255f),   // Gas Giant
            new Color( 170f / 255f, 255f / 255f, 0f / 255f , 255f / 255f),   // Toxic
            new Color( 0f / 255f, 200f / 255f, 255f / 255f , 255f / 255f),   // Ocean
            new Color( 255f / 255f, 187f / 255f, 0f / 255f , 255f / 255f),   // Arid
            new Color( 255f / 255f, 222f / 255f, 133f / 255f , 255f / 255f), // Desert
            new Color( 222f / 255f, 252f / 255f, 247f / 255f , 255f / 255f), // Tundra
            new Color( 104f / 255f, 171f / 255f, 110f / 255f , 255f / 255f), // Continental
            new Color( 0f / 255f, 107f / 255f, 9f / 255f , 255f / 255f),     // Forest
            new Color( 255f / 255f, 17f / 255f, 0f / 255f , 255f / 255f)     // Lava
        };

        public PlanetGraphic PlanetGraphic;
        
        public string planetName;
        
        public readonly int planetIndex;

        public float Radius => m_radius;
        private float m_radius;

        public float Mass => m_mass;
        private float m_mass;

        private Color m_color;
        public Color Color => m_color;

        private float OverallRichness => throw new NotImplementedException();

        public PlanetType planetType;
        
        public PlanetSize planetSize;
        
        public PlanetRichness planetRichness;

        public List<Colony> colonies;

        public void Generate()
        {
            m_mass = UnityEngine.Random.Range( m_massMin, m_massMax );
            m_radius = UnityEngine.Random.Range( m_radiusMin, m_radiusMax );
            m_color = m_colors[ (int) planetType ];
        }
    }
}
