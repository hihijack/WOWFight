using UnityEngine;
using System.Collections;
using System;

public class AnimCtl : MonoBehaviour
{
    public Animator animator;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public AnimatorStateInfo GetCurState()
    {
        return animator.GetCurrentAnimatorStateInfo(0);
    }

    internal void ToIdle()
    {
        animator.SetBool("Move", false);
    }

    internal void ToRunForward(int speed, bool forward)
    {
        animator.SetBool("Move", true);
        animator.SetBool("Forward", forward);
        animator.SetInteger("Speed", speed);
    }

    internal void ToAtkL()
    {
        animator.Play("Attack2H", 0, 0);
    }

    internal void ToStiff()
    {
        animator.Play("CombatWound");
    }

    internal void ToDeath()
    {
        animator.Play("Death");
    }

    internal void ToRoll()
    {
        animator.Play("RollStart",0 ,0);
    }

    internal void ToPower()
    {
        animator.Play("Special2HPower", 0, 0);
    }

    internal void ToAtkHAfter()
    {
        animator.Play("Special2H", 0, 0);
    }

    internal void ToJumpBack()
    {
        animator.Play("JumpEnd", 0, 0);
    }

    internal void ToRush()
    {
        animator.SetBool("Move", true);
        animator.SetBool("Forward", true);
        animator.SetInteger("Speed", 3);
    }

    internal void ToParry()
    {
        animator.Play("Parry", 0, 0);
    }

    public void Play(string anim)
    {
        animator.Play(anim, 0, 0);
    }
}
