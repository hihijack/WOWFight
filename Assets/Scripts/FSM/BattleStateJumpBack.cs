public class BattleStateJumpBack : IBattleState
{
    internal int fDur;

    public BattleStateJumpBack(FSMManager manager) : base(manager)
    {
        type = EBSType.JumpBack;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartJumpBack();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        fDur--;
        if (fDur <= 0)
        {
            fSMManager.ActionIdle();
        }
        else
        {
            fSMManager.ctl.OnBSUpdateJumpBack();
        }
    }

    public override IBattleState ActionIdle()
    {
        return fSMManager.bsIdle;
    }
}
