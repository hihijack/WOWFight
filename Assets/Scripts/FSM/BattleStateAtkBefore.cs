public class BattleStateAtkBefore : IBattleState
{
    public EAtkType atkType;

    public BattleStateAtkBefore(FSMManager manager) : base(manager)
    {
        type = EBSType.AtkBofere;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartAtkBefore(atkType);
    }

    public override void OnEnd()
    {
        base.OnEnd();
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

    public override IBattleState ActionStiff(int frameCount)
    {
        fSMManager.bsStiff.frameDur = frameCount;
        return fSMManager.bsStiff;
    }
}