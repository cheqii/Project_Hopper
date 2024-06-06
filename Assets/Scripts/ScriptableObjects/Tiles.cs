using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "TileData", fileName = "Tiles", order = 0)]
    public class Tiles : ScriptableObject
    {
        public string name;
        public TilesType type;
        public GameObject prefab;

        [Range(0, 10)] 
        public int generateChance;
    }
}
