using UnityEngine;

namespace TilesScript
{
    public class RockTile : TilesBlock
    {
        [Header("Moving Rock")]
        [SerializeField] private MovingRock _movingRock;

        protected override void Start()
        {
            base.Start();
        }

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            _movingRock.AtTop = false;
            _movingRock.AtBottom = false;
        }
    }
}
