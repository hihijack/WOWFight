using UnityEngine;
using System.Collections;
using System;

public class EquipItemCtl : MonoBehaviour
{
    private CharacterCtl _ctl;
    private Collider _myCollider;

    void Awake()
    {
        _myCollider = GetComponent<Collider>();
    }

    
    internal void SetColliderOpenClose(bool open)
    {
        _myCollider.enabled = open;
    }

    public void Init(CharacterCtl ctl)
    {
        this._ctl = ctl;
    }
}
