public class BattleStateIdle : IBattleState
{
    public BattleStateIdle(FSMManager manager) : base(manager)
    {
        type = EBSType.Idle;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartIdle();
    }

    public override IBattleState ActionRun(float h, float v, CharacterCtl lookTarget)
    {
        fSMManager.bsRun.lookTarget = lookTarget;
        return fSMManager.bsRun;
    }

    public override IBattleState ActionAtk(EAtkType atkType)
    {
        fSMManager.bsAtkBefore.atkType = atkType;
        return fSMManager.bsAtkBefore;
    }

    public override IBattleState ActionStiff(int frameCount)
    {
        fSMManager.bsStiff.frameDur = frameCount;
        return fSMManager.bsStiff;
    }

    public override IBattleState ActionRoll(int frameDur, UnityEngine.Vector3 dir)
    {
        fSMManager.bsRoll.dir = dir;
        fSMManager.bsRoll.frameDur = frameDur;
        return fSMManager.bsRoll;
    }

    public override IBattleState ActionPower(int uplimitFrame, int minFreame, bool powering)
    {
        fSMManager.bsPower.maxFrame = uplimitFrame;
        fSMManager.bsPower.minFrame = minFreame;
        fSMManager.bsPower.powering = powering;
        return fSMManager.bsPower;
    }

    public override IBattleState ActionJumpBack(int fDur)
    {
        fSMManager.bsJumpBack.fDur = fDur;
        return fSMManager.bsJumpBack;
    }

    public override IBattleState ActionParry()
    {
        return fSMManager.bsParry;
    }

    public override IBattleState ActionSkill(int skillID)
    {
        fSMManager.bsSKill.skillID = skillID;
        return fSMManager.bsSKill;
    }
}