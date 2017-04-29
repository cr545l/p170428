using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : NonPlayerActor
{
    protected override void CallbackHit( PlayerActor playerActor )
    {
        playerActor.InvokeDamage( Damage );
        InvokeDestroy();
    }
}
