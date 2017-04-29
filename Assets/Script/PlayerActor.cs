using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : GameActor
{
    private float _damage = GameConst._DEFAULT_DAMAGE;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    override protected void InitActor()
    {

    }

    override protected void UpdateActor()
    {

    }

    protected override void CallbackMessage( GameMessage message )
    {
        message._invokeActor.InvokeDamage(_damage);
    }
}
