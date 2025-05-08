using UnityEngine;

namespace Assets._Project.Scripts.Core.Effects
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/SoundLibrary")]
    public class SoundLibrary : ScriptableObject
    {
        public AudioClip carExplosions;
        public AudioClip carDamages;
        public AudioClip turretShots;
        public AudioClip zombieHits;
    }
}