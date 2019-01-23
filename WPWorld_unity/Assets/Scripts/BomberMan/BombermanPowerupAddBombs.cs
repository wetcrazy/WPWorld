using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanPowerupAddBombs : BombermanPowerupBase
{ 
    // GameObject PlayerTarget == Collector
    // Bool isCollected == Powerup is been collected by player

    public override void Effect()
    {
        PlayerTarget.GetComponent<BomberManPlayer>().AddNumBomb();
    }
}
