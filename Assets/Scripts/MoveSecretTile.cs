using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class MoveSecretTile : MoveGroundTile
{
    public override void MoveTileState(RoomState state)
    {
        if(state == RoomState.SecretRoom)
            base.MoveTileState(state);
    }
}
