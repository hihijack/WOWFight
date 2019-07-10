using UnityEngine;

public class BattleStateRoll : IBattleState
{
    internal int frameDur;
    internal Vector3 dir;

    public BattleStateRoll(FSMManager manager) : base(manager)
    {
        type = EBSType.Roll;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartRoll();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override IBattleState ActionIdle()
    {
        return fSMManager.bsIdle;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        fSMManager.ctl.OnBSUpdateRoll(dir);
        frameDur--;
        if (frameDur <= 0)
        {
            fSMManager.ActionIdle();
        }
    }
}