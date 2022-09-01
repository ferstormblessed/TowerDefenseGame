using System;
using UnityEngine;

[CreateAssetMenu (fileName = "WavesData", menuName = "ScriptableObjects/CreateWavesData")]
public class WavesData : ScriptableObject
{
    [Serializable]
    public struct Wave
    {
        public int WeakEnemies;
        public int MidEnemies;
        public int HeavyEnemies;
    }

    public Wave[] Waves;
}
