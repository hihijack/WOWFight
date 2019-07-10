using System;
using UnityEngine;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Entitys;

public class CharacterCtl : MonoBehaviour {

    public float runSpeed;
    public float rollSpeed;
    public float jumpBackSpeed;

    public AnimCtl animCtl;
    public AnimListener animEventListner;
    
    FSMManager mFSMManager;
    CharacterController cc;
    EAtkType _curAtkType;

    private DmgCheckSys _dmgCheckSys;
    
    public bool hurtedEnable = true;

    public RoleUnit RoleUnit { get; set; }

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        animEventListner.Init(this);
        _dmgCheckSys = new DmgCheckSys(this);
    }

    
    public CharacterController GetCharacterController()
    {
        return cc;
    }

    // Use this for initialization
    void Start() {
        mFSMManager = new FSMManager(this);
        mFSMManager.Start();
    }

    // Update is called once per frame
    void Update() {
        ExcCommands();
        mFSMManager.Update();
        _dmgCheckSys.Update();
        mCommands.Clear();
    }

    
    #region Commands
    List<ICommand> mCommands = new List<ICommand>();

    public EAtkType CurAtkType
    {
        get
        {
            return _curAtkType;
        }

        set
        {
            _curAtkType = value;
        }
    }

    /// <summary>
    /// 接受一个命令，来源：InputManager.Update
    /// </summary>
    /// <param name="cMove"></param>
    internal void SendCommand(ICommand command)
    {
        mCommands.Add(command);
    }

    void ClearCommand()
    {
        mCommands.Clear();
    }

    private void ExcCommands()
    {
        for (int i = 0; i < mCommands.Count; i++)
        {
            ICommand command = mCommands[i];
            command.Excut(this);
        }
    }
    #endregion

    #region 状态机
    public FSMManager GetFSM()
    {
        return mFSMManager;
    }

    internal void OnBSUpdateJumpBack()
    {
        Vector3 moveV = -1 * transform.forward * Time.deltaTime * jumpBackSpeed;
        cc.Move(moveV);
    }


    internal void OnBSStartJumpBack()
    {
        animCtl.ToJumpBack();
    }


    internal void OnBSStartPower()
    {
        CurAtkType = EAtkType.AtkH;
        animCtl.ToPower();
    }


    internal void OnBSStartAtkBefore(EAtkType atkType)
    {
        if (atkType == EAtkType.AtkL)
        {
            animCtl.ToAtkL();
        }
        CurAtkType = atkType;
    }


    internal void OnBSUpdateRun(float h, float v, CharacterCtl lookTarget)
    {
        Vector3 moveV = new Vector3(h, -1, 0).normalized * Time.deltaTime * runSpeed;

        int speed = 1;
        if (Mathf.Abs(h) >= 0.6f)
        {
            speed = 2;
        }
        
        if (Math.Abs(h) > 0.01f)
        {
            //行走朝向目标
            float signH = Mathf.Sign(h);
            float signLookTarget = signH;
            if (lookTarget != null && speed == 1)
            {
                signLookTarget = Mathf.Sign(lookTarget.GetCenterPoint().x - this.GetCenterPoint().x);
                transform.forward = new Vector3(signLookTarget, 0, 0);
            }
            else
            {
                transform.forward = new Vector3(signH , 0, 0);
            }
            
            
            animCtl.ToRunForward(speed, signLookTarget * signH > 0);
        }
        
        cc.Move(moveV);
    }

    internal void OnBSUpdateRoll(Vector3 dir)
    {
        dir = dir.normalized;
        transform.forward = dir;
        Vector3 moveV = dir * Time.deltaTime * rollSpeed;
        cc.Move(moveV);
    }


    internal void OnBSStartStiff()
    {
        animCtl.ToStiff();
    }


    internal void OnBSStartRun(float h, float v, CharacterCtl lookTarget)
    {
//        int speed = 1;
//        if (Mathf.Abs(h) >= 0.6)
//        {
//            speed = 2;
//        }
//        animCtl.ToRunForward(speed, true);
    }

    internal void OnBSStartIdle()
    {
        animCtl.ToIdle();
    }

    internal void OnBSStartRoll()
    {
        animCtl.ToRoll();
    }

    internal void OnBSStartAtkAfter()
    {
        if (CurAtkType == EAtkType.AtkH)
        {
            animCtl.ToAtkHAfter();
        }
    }

    internal void OnBSStartParry()
    {
        animCtl.ToParry();
    }

    internal void OnBSUpdateRush(float h, float v)
    {
        Vector3 moveV = new Vector3(h, -1, v).normalized * Time.deltaTime * runSpeed * 1.5f;
        if (h != 0 || v != 0)
        {
            transform.forward = Vector3.Slerp(transform.forward, new Vector3(h, 0f, v), Time.deltaTime * 10);
        }
        cc.Move(moveV);
    }

    internal void OnBSStartRush()
    {
        animCtl.ToRush();
    }

    #endregion

    /// <summary>
    /// 开关伤害检测
    /// </summary>
    /// <param name="v"></param>
    internal void SetDmgCheck(bool open)
    {
       _dmgCheckSys.SetDmgCheckEnable(open);
    }

    internal void OnHitOther(CharacterCtl ctlOther, Vector3 point)
    {
        RoleUnit.DamageTarget(ctlOther.RoleUnit, 10, point);
    }



    public Vector3 GetCenterPoint()
    {
        return transform.position + new Vector3(0, 0.6f, 0f);
    }

    public void HandleDead()
    {
        cc.enabled = false;
        hurtedEnable = false;
        _dmgCheckSys.SetDmgCheckEnable(false);
        animCtl.ToDeath();
    }

    public bool IsInState(EBSType type)
    {
        return GetFSM().CurState.type == type;
    }
}
