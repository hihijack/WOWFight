using System;
using UnityEngine;

public class CommandMove : ICommand
{
    public float h;
    public float v;
    public CharacterCtl lookTarget;
    public override void Excut(CharacterCtl ctl)
    {
        if (Math.Abs(h) > InputManager.AxisMinVal || Math.Abs(v) > InputManager.AxisMinVal)
        {
            ctl.GetFSM().ActionRun(h, v, lookTarget);
        }
        else
        {
            ctl.GetFSM().ActionStopRun();
        }
    }

    public void Set(float h, float v)
    {
        this.h = h;
        this.v = v;
    }
}
