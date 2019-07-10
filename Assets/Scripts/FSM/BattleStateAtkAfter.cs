public class BattleStateAtkAfter : IBattleState
{
    internal EAtkType atkType;

    public BattleStateAtkAfter(FSMManager manager) : base(manager)
    {
        type = EBSType.AtkAfter;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartAtkAfter();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override IBattleState ActionIdle()
    {
        return fSMManager.bsIdle;
    }

    public override IBattleState ActionStiff(int frameCount)
    {
        fSMManager.bsStiff.frameDur = frameCount;
        return fSMManager.bsStiff;
    }
}