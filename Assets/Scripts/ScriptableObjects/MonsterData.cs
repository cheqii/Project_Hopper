using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MonsterData", fileName = "Monster", order = 0)]
    public class MonsterData : ScriptableObject
    {
        public string name;
        public GameObject prefab;
        
        [Range(0, 100)] 
        public int generateChance;
    }
}
