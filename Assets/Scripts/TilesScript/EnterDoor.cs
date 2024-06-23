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

            
            doorEvent.Raise();
            // doorTile.SecretRoomParent.gameObject.SetActive(true);
            // doorTile.NormalLevelParent.gameObject.SetActive(false);
            //
            
            GameManager._instance.secretRoomGenerate.GenerateTile(doorTile.transform.position.y, doorTile.transform.position, doorTile.gameObject);
            // if(doorTile.IsGenerateDone) return;
            // GameManager._instance.secretRoomGenerate.GenerateTile(doorTile.transform.position.y, doorTile.transform.position, doorTile.gameObject);
            // doorTile.IsGenerateDone = true;
        }
    }
}
