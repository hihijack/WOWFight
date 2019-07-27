﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Entitys;

public enum EAtkType
{
    AtkL,
    AtkH
}

public class GameManager : MonoBehaviour
{
    public RoleUnit_Player targetRole;
    public RoleUnit_NPC[] aiCtls;
    public CamCtl camCtl;
    InputManager mInputManager;

    public static GameManager Inst;

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
}
