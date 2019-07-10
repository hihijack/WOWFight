public class BattleStateRush : IBattleState
{
    float h;
    float v;
    public BattleStateRush(FSMManager manager) : base(manager)
    {
        type = EBSType.Rush;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartRush();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        fSMManager.ctl.OnBSUpdateRush(h, v);
    }

    public override IBattleState ActionStopRun()
    {
        return fSMManager.bsIdle;
    }

    public override IBattleState ActionRush(float h, float v)
    {
        this.h = h;
        this.v = v;
        return this;
    }

    public override IBattleState ActionRun(float h, float v, CharacterCtl lookTarget)
    {
        return fSMManager.bsRun;
    }
}