using System;
using Character;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGenerate
{
    [Serializable]
    public class LevelGenerator
    {
        [SerializeField] protected Player _player;

        public Player _Player
        {
            get => _player;
            set => _player = value;
        }

        [Header("Generate Tile Attributes")]
        [Range(0, 10)]
        
        [SerializeField] protected int retainStep = 7;
        public int RetainStep => retainStep;

        [SerializeField] private float tileMaxHeight = 0.2f; 
        [SerializeField] protected float currentHeight = 0f;

        [Header("Moving Coin")]
        [SerializeField] protected GameObject movingCoin;

        private void Start()
        {
            // for (int i = 0; i <= retainStep; i++)
            // {
            //     GenerateTile(i, true);
            // }
        }

        #region -Tile Generate Method-

        public void GeneratePlatformByStep()
        {
            // if(!_player.PlayerCheckGround()) return;
            // if(_player.CurrentRoom == RoomState.SecretRoom) return;
            // var step = retainStep;
            // GenerateTile(++step);
        }

        public virtual void GenerateTile(int step , bool initialGenerate = false)
        {
            
        }

        protected float CheckMaxAndMinHeight(float tileHeight)
        {
            var heightDifference = (Random.value > 0.5f) ? tileMaxHeight : -tileMaxHeight;
            switch (tileHeight)
            {
                case >= 2.8f:
                    tileHeight += -tileMaxHeight;
                    break;
                case <= -2.8f:
                    tileHeight += tileMaxHeight;
                    break;
                default:
                    tileHeight += heightDifference;
                    break;
            }

            return tileHeight;
        }

        #endregion

        #region -Generate Objects-

        protected virtual void GenerateObject(Vector3 position, GameObject tiles, bool secretRoom)
        {
            
        }

        #endregion

        public virtual void ReleaseAllTileInStage()
        {
            
        }

        protected Vector3 RoundVector(Vector3 vector)
        {
            return new Vector3(
                Mathf.Round(vector.x * 10.0f) / 10.0f,
                Mathf.Round(vector.y * 10.0f) / 10.0f,
                Mathf.Round(vector.z * 10.0f) / 10.0f);
        }
    }
}
