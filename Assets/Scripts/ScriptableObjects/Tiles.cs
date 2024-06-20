using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TileData", fileName = "Tiles", order = 0)]
    public class Tiles : ScriptableObject
    {
        public string name;
        public GameObject prefab;

        public GameState state;
        [Range(0, 100)] 
        public int generateChance;
    }
}
