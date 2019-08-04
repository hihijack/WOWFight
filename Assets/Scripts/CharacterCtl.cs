using System;
using UnityEngine;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Entitys;
using DefaultNamespace.GameData;
using DefaultNamespace.SkillPlayable;
using UnityEditor;

public class CharacterCtl : MonoBehaviour {

    public float runSpeed;
    public float rollSpeed;
    public float jumpBackSpeed;

    public bool showBoxDebugInfo;
    
    public AnimCtl animCtl;
    public AnimListener animEventListner;
    
    FSMManager mFSMManager;
    CharacterController cc;
    EAtkType _curAtkType;

    private DmgCheckSys _dmgCheckSys;
    
    public bool hurtedEnable = true;

    public RoleUnit RoleUnit { get; set; }
    
    private SkillDirector _skillDirector;
    
    //事先开辟内存
    Rect[] _rectsDmgCache = new Rect[5];
    Rect[] _rectsBodyCache = new Rect[5];
    
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        animEventListner.Init(this);
        _dmgCheckSys = new DmgCheckSys(this);
        _skillDirector = new SkillDirector(this);
        _skillDirector.onBehavAutoEnd = OnSKillBehavEnd;
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

    private void OnDrawGizmos()
    {
        if (showBoxDebugInfo && Application.isPlaying)
        {
            Rect[] dmgRects = GetWorldDmgRects();
            if (dmgRects != null)
            {
                foreach (var box in dmgRects)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(box.center, box.size);
                }
            }

            Rect[] bodyRects = GetWorldBodyRects();
            if (bodyRects != null)
            {
                foreach (var bodyBox in bodyRects)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(bodyBox.center, bodyBox.size);
                }
            }
        }
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
        _skillDirector.Stop();
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

    
    public void OnBStartSkill(int skillID)
    {
        _skillDirector.CreateBehav(skillID);
       
    }
    
    
    public void OnBSUpdateSkill()
    {
        _skillDirector.Process();
    }
    #endregion

    /// <summary>
    /// 技能行为结束回调;自然结束时触发
    /// </summary>
    private void OnSKillBehavEnd()
    {
       mFSMManager.ActionSkilEnd();
    }

    public SkillDirector GetSkillDirector()
    {
        return _skillDirector;
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
        animCtl.ToDeath();
    }

    public bool IsInState(EBSType type)
    {
        return GetFSM().CurState.type == type;
    }

    /// <summary>
    /// 取身位盒
    /// </summary>
    /// <returns></returns>
    public SKillRect[] GetBodyRects()
    {
        SKillRect[] r = null;
        if (IsInState(EBSType.SKill))
        {
            //技能状态下取技能导演的身位盒
            r = GetSkillDirector().GetCurBodyBox();
        }
        else
        {
            //其他状态下取默认身位盒
            r = GetDefBodyBoxs();
        }

        return r;
    }

    public Rect[] GetWorldBodyRects()
    {
        Rect[] r = null;
        SKillRect[] localRects = GetBodyRects();
        if (localRects != null && localRects.Length > 0)
        {
            for (int i = 0; i < _rectsBodyCache.Length; i++)
            {
                if (i < localRects.Length)
                {
                    _rectsBodyCache[i] = LocalRect2WorldRect(localRects[i]);
                }
                else
                {
                    _rectsBodyCache[i].size = Vector2.zero;
                }
            }

            return _rectsBodyCache;
        }
        return r;
    }

    /// <summary>
    /// 本地盒转世界坐标系
    /// </summary>
    /// <param name="localRect"></param>
    /// <returns></returns>
    private Rect LocalRect2WorldRect(SKillRect localRect)
    {
       var center = transform.localToWorldMatrix.MultiplyPoint(new Vector3(0, localRect.center.y, localRect.center.x));
       Vector3 rectPos = new Vector3(center.x - 0.5f * localRect.size.x, center.y - 0.5f * localRect.size.y, center.z);
       return new Rect(rectPos, localRect.size);
    }

    /// <summary>
    /// 取伤害盒
    /// </summary>
    /// <returns></returns>
    public SKillRect[] GetDmgRects()
    {
        var director = GetSkillDirector();
        return director != null ? director.GetCurDmgBox() : null;
    }

    /// <summary>
    /// 取世界坐标系的伤害盒
    /// </summary>
    /// <returns></returns>
    public Rect[] GetWorldDmgRects()
    {
        Rect[] r = null;
        SKillRect[] localRects = GetDmgRects();
        if (localRects != null && localRects.Length > 0)
        {
            for (int i = 0; i < _rectsDmgCache.Length; i++)
            {
                if (i < localRects.Length)
                {
                    _rectsDmgCache[i] = LocalRect2WorldRect(localRects[i]);
                }
                else
                {
                    _rectsDmgCache[i].size = Vector2.zero;
                }
            }

            return _rectsDmgCache;
        }
        return r;
    }
    
    /// <summary>
    /// 默认状态下的身位盒
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private SKillRect[] GetDefBodyBoxs()
    {
        SKillRect[] r = null;
        SKillDataNode data = GameDataMgr.Inst.skillTable.GetData(1);
        if (data.isValid())
        {
            var skillBoxInfo = data.FindBodyBoxInfo(0);
            if (skillBoxInfo.isValid())
            {
                r = skillBoxInfo.rects;
            }
        }

        return r;
    }
}
