using System;

public class FSMManager
{
    public CharacterCtl ctl;
    IBattleState _curState;

    public BattleStateIdle bsIdle;
    public BattleStateRun bsRun;
    public BattleStateRush bsRush;
    public BattleStatePower bsPower;
    public BattleStateAtkBefore bsAtkBefore;
    public BattleStateAtkAfter bsAtkAfter;
    public BattleStateParry bsParry;
    public BattleStateRoll bsRoll;
    public BattleStateStiff bsStiff;
    public BattleStateJumpBack bsJumpBack;

    public IBattleState CurState
    {
        get
        {
            return _curState;
        }

        set
        {
            _curState = value;
        }
    }

    public FSMManager(CharacterCtl ctl)
    {
        this.ctl = ctl;
        bsIdle = new BattleStateIdle(this);
        bsRun = new BattleStateRun(this);
        bsAtkBefore = new BattleStateAtkBefore(this);
        bsAtkAfter = new BattleStateAtkAfter(this);
        bsRush = new BattleStateRush(this);
        bsPower = new BattleStatePower(this);
        bsParry = new BattleStateParry(this);
        bsRoll = new BattleStateRoll(this);
        bsStiff = new BattleStateStiff(this);
        bsJumpBack = new BattleStateJumpBack(this);
    }

    public void Update()
    {
        if (CurState != null)
        {
            CurState.OnUpdate();
        }
    }

    public void Clear()
    {
        CurState = null;
    }

    public void Start()
    {
        CurState = bsIdle;
        CurState.OnStart();
    }

    void ChangeState(IBattleState next)
    {
        if (next != null)
        {
            if (CurState != null)
            {
                CurState.OnEnd();
            }
            CurState = next;
            CurState.OnStart();
        }
    }

    #region Actions
    internal void ActionAtkAfter(EAtkType atkType)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionAtkAfter(atkType);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionIdle()
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionIdle();
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionStopRun()
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionStopRun();
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }


    public void ActionRun(float h, float v, CharacterCtl lookTarget)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionRun(h, v, lookTarget);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionAtkBefore(EAtkType atkType)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionAtk(atkType);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionStiff(int frameCount)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionStiff(frameCount);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionRoll(int frameDur, UnityEngine.Vector3 dir)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionRoll(frameDur, dir);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionPower(int uplimitFrame, int minFrame, bool powering)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionPower(uplimitFrame, minFrame, powering);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionParry()
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionParry();
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionJumpBack(int fDur)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionJumpBack(fDur);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    internal void ActionRush(float h, float v)
    {
        if (CurState != null)
        {
            IBattleState next = CurState.ActionRush(h, v);
            if (next != CurState)
            {
                ChangeState(next);
            }
        }
    }

    #endregion
}
