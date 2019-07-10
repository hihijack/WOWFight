using System;

public class CommandAtkH : ICommand
{
    public bool pressing;
    public override void Excut(CharacterCtl ctl)
    {
        ctl.GetFSM().ActionPower(60, 19, pressing);
    }

    public void Set(bool pressing)
    {
        this.pressing = pressing;
    }
}