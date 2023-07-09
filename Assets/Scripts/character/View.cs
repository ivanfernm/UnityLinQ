using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class View: IAnimator
{
    Animator _animator;
    Action AnimationState;
    public View(Animator anim)
    { _animator = anim; }


    #region IAnimator
    public void Idle()
    {
        _animator.SetInteger("AnimationState", 0);
    }

    public void Attack()
    {
        _animator.SetInteger("AnimationState", 2);
    }

    public void Walking()
    {
        _animator.SetInteger("AnimationState", 1);
    }

    public void Dead()
    {
        _animator.SetInteger("AnimationState", 3);
    }

    public void ReceivesDamage()
    {
        _animator.SetInteger("AnimationState", 4);
    }
    #endregion

    #region animatorVoids
    public void OnAwake()
    {
        AnimationState = Idle;
    }
    public void OnUpdate()

    { 
        AnimationState();
    }

    public void OnWalk()
    {
        AnimationState = Walking;
    }

    public void OnAttack()
    {
        AnimationState = Attack;

    }
    public void OnAturd()
    {
        AnimationState = ReceivesDamage;
    }
    #endregion
    


}
