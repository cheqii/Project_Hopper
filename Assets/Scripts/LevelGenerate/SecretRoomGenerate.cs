using System;
using System.Collections.Generic;
using ObjectPool;
using TilesScript;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGenerate
{
    [Serializable]
    public class SecretRoomGenerate: LevelGenerator
    {
        [SerializeField] private Transform secretRoomParent;
        [SerializeField] private GameObject fallingTile;
        [SerializeField] private GameObject exitDoor;
        public override void GenerateTile(int step, bool initialGenerate = false)
        {
            base.GenerateTile(step, initialGenerate);
        }

        public void GenerateTile(float doorYPos, Vector3 doorCurrentTransform, GameObject startDoor)
        {
            if(_player.CurrentRoom == RoomState.NormalRoom) return;
            var randomTile = Random.Range(3, retainStep);

            var enterRoomTile = PoolManager.SpawnObject(startDoor, RoundVector(doorCurrentTransform), Quaternion.identity);
            enterRoomTile.transform.SetParent(secretRoomParent);

            var enterDoorTile = enterRoomTile.GetComponent<TilesBlock>();
            enterDoorTile._Player = _player;
        
            for (int i = 2; i <= randomTile; i++)
            {
                var tileHeightPossibility = (!(Random.value > 0.5f));
            
                if (!tileHeightPossibility)
                    doorYPos += 0;
                else                                                                         
                    doorYPos = CheckMaxAndMinHeight(doorYPos);

                var tilePos = new Vector3(i, doorYPos, 0f);
                var newTile = PoolManager.SpawnObject(fallingTile, RoundVector(tilePos), Quaternion.identity);
                newTile.transform.SetParent(secretRoomParent);
            
                var tile = newTile.GetComponent<TilesBlock>();
                tile._Player = _player;
            
                GenerateObject(tilePos, newTile);
            }

            var exitRoomTile = PoolManager.SpawnObject(exitDoor, RoundVector(new Vector3(randomTile + 1, doorYPos, 0f)), Quaternion.identity);
            exitRoomTile.transform.SetParent(secretRoomParent);
        
            var doorTile = exitRoomTile.GetComponent<TilesBlock>();
            doorTile._Player = _player;
        }

        protected override void GenerateObject(Vector3 position, GameObject tiles)
        {
            var objectPos = RoundVector(new Vector3(tiles.transform.position.x, position.y + 1));
            var newMovingCoin = PoolManager.SpawnObject(movingCoin, RoundVector(objectPos), Quaternion.identity);
            newMovingCoin.transform.SetParent(tiles.transform);
        }
        
        public override void ReleaseAllTileInStage()
        {
            if(_player.CurrentRoom == RoomState.SecretRoom) return;
            foreach (Transform child in secretRoomParent)
            {
                PoolManager.ReleaseObject(child.gameObject);
            }
        }
    }
}
