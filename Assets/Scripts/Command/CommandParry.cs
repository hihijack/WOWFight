using System;

public class CommandParry : ICommand
{
    public override void Excut(CharacterCtl ctl)
    {
        ctl.GetFSM().ActionParry();
    }
}