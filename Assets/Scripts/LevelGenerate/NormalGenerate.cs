using System;
using System.Collections.Generic;
using Character.Monster;
using ObjectPool;
using ScriptableObjects;
using TilesScript;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGenerate
{
    [Serializable]
    public class NormalGenerate : LevelGenerator
    {
        #region -Tiles-

        [Space]
        [SerializeField] private Transform normalTileParent;
        [SerializeField] private GameObject normalTilePrefab;

        #region -Tile Stage with Dictionary-

        [Space] 
        [SerializeField] private TileStageDictionary tileStage;
        public TileStageDictionary TileStage => tileStage;
        
        private Dictionary<GameState, TileStageProperty> _dictionary;
        public Dictionary<GameState, TileStageProperty> _Dictionary
        {
            get => _dictionary;
            set => _dictionary = value;
        }

        #endregion

        #endregion

        #region -Objects-

        [Header("Generate Object")]
        [SerializeField] private List<ObjectData> allObject;

        #endregion

        public override void GenerateTile(int step, bool initialGenerate = false)
        {
            base.GenerateTile(step, initialGenerate);
            var tilePrefab = normalTilePrefab;
            if (!initialGenerate)
            {
                var tileHeightPossibility = (!(Random.value > 0.5f));
            
                if (!tileHeightPossibility)
                    currentHeight += 0;
                else                                                                         
                    currentHeight = CheckMaxAndMinHeight(currentHeight);

                tilePrefab = GetRandomTile();
            }

            var position = new Vector3(step, currentHeight, 0f);
            var newTile = PoolManager.SpawnObject(tilePrefab, RoundVector(position), Quaternion.identity);
            newTile.transform.SetParent(PoolManager.Instance.root);
        
            var tile = newTile.GetComponent<TilesBlock>();

            tile._Player = _player;
            tile.SetToInitialTile(position);

            if(initialGenerate) return;
            GenerateObject(position, newTile, false);
            GenerateMonsterOnTile(position, newTile);
        }
        
        private List<Tiles> GetTileByLevel(GameState state)
        {
            if (_dictionary.TryGetValue(state, out var value))
            {
                return value.tiles;
            }
            return null;
        }
        
        private GameObject GetRandomTile()
        {
            var totalChance = 0;
            
            foreach (var tiles in GetTileByLevel(GameManager._instance.currentGameState))
            {
                totalChance += tiles.generateChance;
            }

            var rand = Random.Range(0, totalChance);
        
            foreach (var tiles in GetTileByLevel(GameManager._instance.currentGameState))
            {
                if (rand < tiles.generateChance)
                {
                    return tiles.prefab;
                }

                rand -= tiles.generateChance;
            }

            return normalTilePrefab;
        }

        #region -Generate Monsters-

        private void GenerateMonsterOnTile(Vector3 position, GameObject tiles)
        {
            if (tiles == null) return;
            var tileCheck = tiles.GetComponent<TilesBlock>();
            var checkForGenerate = (!(Random.value > 0.85f));

            if(tileCheck.Type != TilesType.Normal || tileCheck.ObjectOnTile != null) return;
            if(checkForGenerate) return;
            
            var monsterPos = RoundVector(new Vector3(tiles.transform.position.x, position.y + 1));
            var newMonster = PoolManager.SpawnObject(GetRandomMonster(), RoundVector(monsterPos), Quaternion.identity);

            var monster = newMonster.GetComponent<Monster>();

            monster.SetToInitialMonster(monsterPos);
            newMonster.transform.SetParent(tiles.transform);
        }

        private List<MonsterData> GetMonsterByLevel(GameState state)
        {
            if (_dictionary.TryGetValue(state, out var value))
            {
                return value.monsters;
            }
            return null;
        }

        private GameObject GetRandomMonster()
        {
            var totalChance = 0;
            foreach (var monster in GetMonsterByLevel(GameManager._instance.currentGameState))
            {
                totalChance += monster.generateChance;
            }

            var rand = Random.Range(0, totalChance);
            foreach (var monster in GetMonsterByLevel(GameManager._instance.currentGameState))
            {
                if (rand < monster.generateChance)
                {
                    return monster.prefab;
                }

                rand -= monster.generateChance;
            }

            return null;
        }

        #endregion

        #region -Generate Objects-

        protected override void GenerateObject(Vector3 position, GameObject tiles, bool secretRoom)
        {
            if (tiles == null) return;

            var objectPos = RoundVector(new Vector3(tiles.transform.position.x, position.y + 1));
        
            var tileCheck = tiles.GetComponent<TilesBlock>();
            var checkForGenerate = (!(Random.value > 0.85f));

            if(tileCheck.Type != TilesType.Normal || tileCheck.ObjectOnTile != null) return;
            if(checkForGenerate) return;

            var newObject = PoolManager.SpawnObject(GetRandomObject(), RoundVector(objectPos), Quaternion.identity);
            var _object = newObject.GetComponent<ObjectInGame.ObjectInGame>();
        
            newObject.transform.SetParent(tiles.transform);
            _object._Player = _player;
            _object.SetToInitialObject(objectPos);
        }

        private GameObject GetRandomObject()
        {
            var totalChance = 0;
            foreach (var objectData in allObject)
            {
                totalChance += objectData.generateChance;
            }

            var rand = Random.Range(0, totalChance);
            foreach (var objectData in allObject)
            {
                if (rand < objectData.generateChance)
                {
                    return objectData.prefab;
                }

                rand -= objectData.generateChance;
            }
        
            return allObject[0].prefab;
        }

        #endregion

        public override void ReleaseAllTileInStage()
        {
            foreach (Transform child in normalTileParent)
            {
                PoolManager.ReleaseObject(child.gameObject);
            }
        }
    }
}
