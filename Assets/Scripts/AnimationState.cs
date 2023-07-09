using UnityEngine;

public class AnimationState : IAction
{
    //default
    //Idle=0 Walking=1 Attack=2 Dead=3

    Animator _animator;
    string _stateName = "AnimationState";
    int _state;

    public AnimationState(Animator animator, string boolean, int state)
    {
        _animator = animator;
        _stateName = boolean;
        _state = state;
    }
    public void Action()
    {
        _animator.SetInteger(_stateName, 1);
    }
}
