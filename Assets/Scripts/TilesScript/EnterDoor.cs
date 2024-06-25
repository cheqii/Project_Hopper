using Interface;
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

            GameManager._instance.SetTile(false, true);

            GameManager._instance.secretRoomGenerate.GenerateTile(doorTile.transform.position.y, doorTile.transform.position, doorTile.gameObject);
        }
    }
}
