using UnityEngine;

public class BattleStateRun : IBattleState
{
    float h;
    float v;
    public CharacterCtl lookTarget;

    public BattleStateRun(FSMManager manager) : base(manager)
    {
        type = EBSType.Run;
    }

    public override void OnStart()
    {
        base.OnStart();
        fSMManager.ctl.OnBSStartRun(h, v, lookTarget);
    }

    public override IBattleState ActionStopRun()
    {
        return fSMManager.bsIdle;
    }

    public override IBattleState ActionRun(float h, float v, CharacterCtl lookTarget)
    {
        this.h = h;
        this.v = v;
        this.lookTarget = lookTarget;
        return this;
    }

    public override IBattleState ActionAtk(EAtkType atkType)
    {
        fSMManager.bsAtkBefore.atkType = atkType;
        return fSMManager.bsAtkBefore;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        fSMManager.ctl.OnBSUpdateRun(h, v, lookTarget);
    }

    public override IBattleState ActionRoll(int frameDur, UnityEngine.Vector3 dir)
    {
        fSMManager.bsRoll.dir = dir;
        fSMManager.bsRoll.frameDur = frameDur;
        return fSMManager.bsRoll;
    }

    public override IBattleState ActionPower(int uplimitFrame, int minFreamd, bool powering)
    {
        fSMManager.bsPower.maxFrame = uplimitFrame;
        fSMManager.bsPower.minFrame = minFreamd;
        fSMManager.bsPower.powering = powering;
        return fSMManager.bsPower;
    }

    public override IBattleState ActionRush(float h, float v)
    {
        return fSMManager.bsRush;
    }

    public override IBattleState ActionParry()
    {
        return fSMManager.bsParry;
    }

    public override IBattleState ActionStiff(int frameCount)
    {
        fSMManager.bsStiff.frameDur = frameCount;
        return fSMManager.bsStiff;
    }
}