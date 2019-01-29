using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanHardBreakable : BombermanBreakable
{
	public BombermanHardBreakable()
    {
        isDestroyed = false;
        NumHits = 2;
    }
}
