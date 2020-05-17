using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace OrbitalSystem.Element
{
    [System.Serializable]
    public class OrbitalModel
    {
        public float HealthPoint { get; set; }
        public float RotateSpeed { get; set; }
        public float Distance { get; set; }
        public float Radius { get; set; }
        public float MoveSpeed { get; set; }
        public float GroundParam { get; set; }

        public Owner Owner { get; set; }

        public SerializableVector SerializableSize { get; set; } = new SerializableVector();
        public SerializableVector SerializablePosition { get; set; } = new SerializableVector();
        public SerializableVector SerializableWaterColor { get; set; } = new SerializableVector();
        public SerializableVector SerializableGroundColor { get; set; } = new SerializableVector();
        public SerializableVector SerializableAtmosphereColor { get; set; } = new SerializableVector();
        
        [JsonIgnore]
        public Vector3 Size 
        { 
            get => SerializableSize.ToVector();
            set => SerializableSize = new SerializableVector().FromVector(value);
        }
        [JsonIgnore]
        public Vector3 Position 
        { 
            get => SerializablePosition.ToVector();
            set => SerializablePosition = new SerializableVector().FromVector(value);
        }
        [JsonIgnore]
        public Color WaterColor 
        { 
            get => SerializableWaterColor.ToColor();
            set => SerializableWaterColor = new SerializableVector().FromColor(value);
        }
        [JsonIgnore]
        public Color GroundColor 
        { 
            get => SerializableGroundColor.ToColor();
            set => SerializableGroundColor = new SerializableVector().FromColor(value);
        }
        [JsonIgnore]
        public Color AtmosphereColor 
        { 
            get => SerializableAtmosphereColor.ToColor();
            set => SerializableAtmosphereColor = new SerializableVector().FromColor(value);
        }
    }
}