using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Entitys;
using DefaultNamespace.Triger;
using UI;

public enum EAtkType
{
    AtkL,
    AtkH
}

public class GameManager : MonoBehaviour
{
    public RoleUnit_Player targetRole;
    public RoleUnit_NPC[] aiCtls;
    public AudioSource bgm;
    public CamCtl camCtl;
    InputManager mInputManager;

    public static GameManager Inst;

    public BaseTrigger curStayTrigger;
    
    void Awake()
    {
        Inst = this;
        mInputManager = new InputManager();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mInputManager.OnUpdate();
    }

    public void ReStartGame()
    {
       Application.LoadLevel("main");
    }

    public void OnUnitDie(RoleUnit roleUnit)
    {
        if (roleUnit == targetRole)
        {
            UIMgr.Inst.ShowUIGameOver();
            bgm.Stop();
        }else if (roleUnit == aiCtls[0])
        {
            UIMgr.Inst.ShowUIGameWin();
            bgm.Stop();
        }
    }

    public void OnSummonBoss()
    {
        UIMgr.Inst.TogUIControlTip(false);
        UIMgr.Inst.TogUISummonTip(false);
        aiCtls[0].gameObject.SetActive(true);
        Inst.targetRole.atkTarget = aiCtls[0];
        UIMgr.Inst.RefreshPlayerInfo();
        bgm.Play();
    }
}
