public class BattleStatePower : IBattleState
{
    internal int maxFrame;
    internal int minFrame;
    internal bool powering;

    public BattleStatePower(FSMManager manager) : base(manager)
    {
        type = EBSType.Power;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartPower();
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        minFrame--;
        if (minFrame <= 0)
        {
            if (!powering)
            {
                fSMManager.ActionAtkAfter(EAtkType.AtkH);
                return;
            }
        }
        maxFrame--;
        if (maxFrame <= 0)
        {
            fSMManager.ActionAtkAfter(EAtkType.AtkH);
        }
    }

    public override IBattleState ActionPower(int uplimitFrame, int minFreamd, bool powering)
    {
        this.powering = powering;
        return null;
    }

    public override IBattleState ActionAtkAfter(EAtkType atkType)
    {
        fSMManager.bsAtkAfter.atkType = atkType;
        return fSMManager.bsAtkAfter;
    }

    public override IBattleState ActionRoll(int frameDur, UnityEngine.Vector3 dir)
    {
        fSMManager.bsRoll.dir = dir;
        fSMManager.bsRoll.frameDur = frameDur;
        return fSMManager.bsRoll;
    }
}