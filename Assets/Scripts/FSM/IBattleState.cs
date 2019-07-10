using System;

public enum EBSType
{
    Idle,
    Stiff,
    AtkBofere,
    AtkAfter,
    Parry,
    Run,
    Rush,
    Roll,
    JumpBack,
    Power
}

public class IBattleState
{
    public EBSType type;
    protected FSMManager fSMManager;

    public IBattleState(FSMManager manager)
    {
        this.fSMManager = manager;
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnStart()
    {
    }

    public virtual void OnEnd()
    {

    }

    public virtual IBattleState ActionRun(float h, float v, CharacterCtl lookTarget)
    {
        return null;
    }

    public virtual IBattleState ActionIdle()
    {
        return null;
    }

    public virtual IBattleState ActionAtk(EAtkType atkType)
    {
        return null;
    }

    public virtual IBattleState ActionAtkAfter(EAtkType atkType)
    {
        return null;
    }

    public virtual IBattleState ActionStopRun()
    {
        return null;
    }

    public virtual IBattleState ActionStiff(int frameCount)
    {
        return null;
    }

    public virtual IBattleState ActionRoll(int frameDur, UnityEngine.Vector3 dir)
    {
        return null;
    }

    public virtual IBattleState ActionPower(int uplimitFrame, int minFreamd, bool powering)
    {
        return null;
    }

    public virtual IBattleState ActionJumpBack(int fDur)
    {
        return null;
    }

    public virtual IBattleState ActionRush(float h, float v)
    {
        return null;
    }

    public virtual IBattleState ActionParry()
    {
        return null;
    }
}
