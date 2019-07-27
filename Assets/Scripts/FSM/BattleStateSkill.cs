namespace DefaultNamespace.FSM
{
    public class BattleStateSkill : IBattleState
    {
        public int skillID;
        public BattleStateSkill(FSMManager manager) : base(manager)
        {
            type = EBSType.SKill;
        }

        public override void OnStart()
        {
            base.OnStart();
            fSMManager.ctl.OnBStartSkill(skillID);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            fSMManager.ctl.OnBSUpdateSkill();
        }

        public override IBattleState ActionSkillEnd()
        {
            return fSMManager.bsIdle;
        }

        public override IBattleState ActionStiff(int frameCount)
        {
            fSMManager.bsStiff.frameDur = frameCount;
            return fSMManager.bsStiff;
        }
    }
}