﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NonPlayer : GameActor {

    private Animator animator;
    private float playSpeed;

	protected override void InitActor ()
    {
        animator = GetComponent<Animator>();
        playSpeed = 0.0f;
        animator.speed = playSpeed;
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
        //임시로 Destroy
        Destroy(gameObject);
    }

    public void PlayAnimation()
    {
        animator.speed = playSpeed;
    }

    public void SetPlaySpeed(float speed)
    {
        playSpeed = speed;
    }

    public void SetAnimatorController(RuntimeAnimatorController controller)
    {
        animator.runtimeAnimatorController = controller;
    }

    protected override void CallbackMessage(GameMessage message)
    {
        
    }

    
}
