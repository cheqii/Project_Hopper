using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class MoveNormalTile : MoveGroundTile
{
    public override void MoveTileState(RoomState state)
    {
        if(state == RoomState.NormalRoom)
            base.MoveTileState(state);
    }
}
