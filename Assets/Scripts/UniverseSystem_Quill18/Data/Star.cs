using UnityEngine;

namespace Starlight
{

    public enum StarClass { M, K, G, F, A, B, O }
    public class Star
    {
        public StarClass starClass;

        // Star Class                                 --M--   --K--   --G--   --F--   --A--   --B--   --O--
        private static readonly float[] m_massMin = { 0.08f,  0.45f,  0.8f,   1.04f,  1.4f,   2.1f,   16f };
        private static readonly float[] m_massMax = { 0.45f,  0.8f,   1.04f,  1.4f,   2.1f,   16f,    265f };

        // Star Class                                   --M--    --K--   --G--   --F--   --A--   --B--   --O--
        private static readonly float[] m_radiusMin = { 0.35f,   0.7f,   0.96f,  1.15f,  1.4f,   1.8f,   6.6f };
        private static readonly float[] m_radiusMax = { 0.7f,    0.96f,  1.15f,  1.4f,   1.8f,   6.6f,   15f };

        // Star Class                                       --M--   --K--   --G--   --F--   --A---    --B---    --O---
        private static readonly int[] m_temperatureMin = {  2400,   3700,   5200,   6000,   7500,     10_000,   30_000 };
        private static readonly int[] m_temperatureMax = {  3700,   5200,   6000,   7500,   10_000,   30_000,   50_000 };

        private static readonly Color[] m_colors = {
            new Color( 255f / 255f, 204f / 255f, 111f / 255f, 255f / 255f ), // M
            new Color( 255f / 255f, 210f / 255f, 161f / 255f, 255f / 255f ), // K
            new Color( 255f / 255f, 244f / 255f, 234f / 255f, 255f / 255f ), // G
            new Color( 248f / 255f, 247f / 255f, 255f / 255f, 255f / 255f ), // F
            new Color( 202f / 255f, 215f / 255f, 255f / 255f, 255f / 255f ), // A
            new Color( 170f / 255f, 191f / 255f, 255f / 255f, 255f / 255f ), // B
            new Color( 155f / 255f, 176f / 255f, 255f / 255f, 255f / 255f )  // O
        };

        public string starName;

        public float Radius => m_radius;
        private float m_radius;

        public float Mass => m_mass;
        private float m_mass;

        public int Temperature => m_temperature;
        private int m_temperature;

        public Color Color => m_color;
        private Color m_color;

        public void Generate( int numOfStarClass )
        {
            int classIndex = (int) starClass;

            m_mass = Random.Range( m_massMin[ classIndex ], m_massMax[ classIndex ] );
            m_radius = Random.Range( m_radiusMin[ classIndex ], m_radiusMax[ classIndex ] );
            m_temperature = (int) System.Math.Round( Random.Range( m_temperatureMin[ classIndex ], m_temperatureMax[ classIndex ] ) / 100m, 0) * 100;
            m_color = m_colors[ classIndex ];

            starName = GenerateName( numOfStarClass );
        }

        private string GenerateName( int numOfStarClass )
        {
            int classIndex = (int) starClass;

            string output = System.Enum.GetName( typeof( StarClass ), starClass );

            output += (GetNormalizedBetweenInverted( Temperature, m_temperatureMin[ classIndex ], m_temperatureMax[ classIndex ] ) * 9f).ToString("N1");

            if (output.EndsWith( "0" ))
            {
                output = output.Substring( 0, 2 );
            }

            output += $" - {UnityEngine.Random.Range(1, numOfStarClass * 3)}";

            return output.Replace(',', '.');
        }

        private float GetNormalizedBetweenInverted( int value, int min, int max )
        {
            return (float)(max - value) / (max - min);
        }
    }
}
