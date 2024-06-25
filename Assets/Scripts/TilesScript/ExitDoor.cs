using Interface;
using ObjectInGame;
using UnityEngine;

namespace TilesScript
{
    public class ExitDoor : Door
    {
        public override void EnterTheDoor()
        {
            base.EnterTheDoor();
            _Player.CurrentRoom = RoomState.NormalRoom;

            GameManager._instance.SetTile(true, false);
            
            _Player.transform.position = new Vector3(_Player.transform.position.x, transform.position.y + 0.5f);
        }
    }
}
