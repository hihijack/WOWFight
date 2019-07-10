using System;
using UnityEngine;

public class CommandRoll : ICommand
{
    public UnityEngine.Vector3 dir;
    public override void Excut(CharacterCtl ctl)
    {
        if (dir == UnityEngine.Vector3.zero)
        {
            //后跳
            ctl.GetFSM().ActionJumpBack(15);
        }
        else
        {
            ctl.GetFSM().ActionRoll(20, dir);
        }
    }

    public void Set(Vector3 dir)
    {
        this.dir = dir;
    }
}
