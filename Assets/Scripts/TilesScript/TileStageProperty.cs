using System.Collections.Generic;
using ScriptableObjects;

namespace TilesScript
{
    [System.Serializable]
    public class TileStageProperty
    {
        public List<Tiles> tiles;
        public List<MonsterData> monsters;

        public TileStageProperty(List<Tiles> tile, List<MonsterData> monsterData)
        {
            tiles = tile;
            monsters = monsterData;
        }
    }

    [System.Serializable]
    public class TileStageDictProperty
    {
        public string levelName;
        public GameState key;
        public TileStageProperty tileStageProperty;
    }

    [System.Serializable]
    public class TileStageDictionary
    {
        public List<TileStageDictProperty> tileDict;

        public Dictionary<GameState, TileStageProperty> ToDictionary()
        {
            Dictionary<GameState, TileStageProperty> newStage = new Dictionary<GameState, TileStageProperty>();

            foreach (var dictionary in tileDict)
            {
                newStage.Add(dictionary.key, dictionary.tileStageProperty);
            }
            return newStage;
        }
    }
}
