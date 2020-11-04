using UnityEngine;

namespace Test
{
    [CreateAssetMenu(fileName = "WorldData", menuName = "ScriptableObjects/Data/WorldData", order = 1)]
    public class WorldData : ScriptableObject
    {
        public Star[] Stars;
        public bool ShowShip;
    }

    [System.Serializable]
    public struct Star
    {
        public Vector2 StartPos;
        [Range(20, 50)]
        public float Mass;
        public Color RenderColor;
        [Range(3, 15)]
        public int Segments;
        public Satellite[] Satellites;
    }

    [System.Serializable]
    public struct Satellite
    {
        [Tooltip("Use same number of segments than his Star?")]
        public bool SameShape;
        [Range(3, 15), Tooltip("If SameShape, Segments = Star.Segments")]
        public int Segments;
        [Tooltip("Use same render color than his Star?")]
        public bool SameColor;
        [Tooltip("If SameColor, RenderColor = Star.RenderColor")]
        public Color RenderColor;
    }
}