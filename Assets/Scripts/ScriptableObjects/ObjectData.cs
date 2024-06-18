using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ObjectData", fileName = "Object", order = 0)]
    public class ObjectData : ScriptableObject
    {
        public string name;
        public GameObject prefab;

        [Range(0, 100)] 
        public int generateChance;
    }
}
