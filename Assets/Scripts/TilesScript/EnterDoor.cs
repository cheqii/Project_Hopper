using LevelGenerate;
using ObjectInGame;
using UnityEngine;

namespace TilesScript
{
    public class EnterDoor : Door
    {
        public override void EnterTheDoor()
        {
            base.EnterTheDoor();
            doorTile.AlreadyEnter = true;
            _Player.CurrentRoom = RoomState.SecretRoom;

            doorTile.SecretRoomParent.gameObject.SetActive(true);
            doorTile.NormalLevelParent.gameObject.SetActive(false);

            if(doorTile.IsGenerateDone) return;
            // LevelGenerator.Instance.GenerateTileSecretRoom(doorTile.transform.position.y, doorTile.transform.position, doorTile.gameObject);
            doorTile.IsGenerateDone = true;
        }
    }
}
