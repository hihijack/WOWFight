using System;
using UnityEngine;

public class CommandRush : ICommand
{
    public float h;
    public float v;
    internal Vector3 dir;

    public override void Excut(CharacterCtl ctl)
    {
        if (h != 0 || v != 0)
        {
            ctl.GetFSM().ActionRush(h, v);
        }
        else
        {
            ctl.GetFSM().ActionStopRun();
        }
    }
}
