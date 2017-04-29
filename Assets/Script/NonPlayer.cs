using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NonPlayer : GameActor {

    private Animator animator;

	protected override void InitActor ()
    {
        Init( 0.1f);
    }

    public void Init(float speed )
    {
        animator = GetComponent<Animator>();
        if( null != animator )
        {
            animator.speed = speed;
        }
    }

    protected override void UpdateActor()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        InvokeMessage(new GameActorMessage {
            _targetType = eMessageType.NonPlayerAttack_FromUser,
            _targetActor = null});
    }

    public void SetPlaySpeed(float speed)
    {
        animator.speed = speed;
    }

    public void SetAnimatorController(RuntimeAnimatorController controller)
    {
        animator.runtimeAnimatorController = controller;
    }

    protected override void CallbackMessage(GameMessage message)
    {
        
    }

    protected override void CallbackDeath(GameActor target)
    {
        Destroy(gameObject);
    }
}
