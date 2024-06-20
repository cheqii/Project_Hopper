using Interface;
using ObjectInGame;
using UnityEngine;

namespace TilesScript
{
    public class ExitDoor : Door
    {
        [SerializeField] private Nf_GameEvent exitRoomEvent;
        
        public override void EnterTheDoor()
        {
            base.EnterTheDoor();
            _Player.CurrentRoom = RoomState.NormalRoom;
            doorTile.NormalLevelParent.gameObject.SetActive(true);
            doorTile.SecretRoomParent.gameObject.SetActive(false);
            
            exitRoomEvent.Raise();
            
            _Player.transform.position = new Vector3(_Player.transform.position.x, transform.position.y + 2);
        }
    }
}
