namespace DefaultNamespace.Command
{
    public class CommandSkill : ICommand
    {
        public int skillID;
        public override void Excut(CharacterCtl ctl)
        {
            if (skillID > 0)
            {
                ctl.GetFSM().ActionSkill(skillID);
            }
        }
    }
}