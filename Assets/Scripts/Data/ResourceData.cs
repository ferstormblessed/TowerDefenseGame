using System;
using UnityEngine;

[CreateAssetMenu (fileName = "WeaponsData", menuName = "ScriptableObjects/CreateResourcesData")] 
public class ResourceData : ScriptableObject
{
    [Serializable]
    public struct WeaponConfig
    {
        public string WeaponName;
        public int WeaponCost;
    }

    public WeaponConfig[] WeaponsCosts;


}
