using System;

public class CommandAtkL : ICommand
{
    public override void Excut(CharacterCtl ctl)
    {
        ctl.GetFSM().ActionAtkBefore(EAtkType.AtkL);
    }
}
