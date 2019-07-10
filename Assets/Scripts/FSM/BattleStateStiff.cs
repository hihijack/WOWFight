/// <summary>
/// 受击硬直状态
/// </summary>
public class BattleStateStiff : IBattleState
{
    public int frameDur;
    int curFrame;
    public BattleStateStiff(FSMManager manager) : base(manager)
    {
        type = EBSType.Stiff;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartStiff();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        frameDur--;
        if (frameDur <= 0)
        {
            fSMManager.ActionIdle();
        }
    }

    public override IBattleState ActionIdle()
    {
        return fSMManager.bsIdle;
    }
}