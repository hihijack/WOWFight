using UnityEngine;
using System.Collections;
using System;

public class AnimListener : MonoBehaviour
{
    CharacterCtl ctl;

    internal void Init(CharacterCtl ctl)
    {
        this.ctl = ctl;
    }

    public void OnAtkBeforeEnd()
    {
        ctl.GetFSM().ActionAtkAfter(ctl.CurAtkType);
    }

    public void OnAtkAfterEnd()
    {
        ctl.GetFSM().ActionIdle();
    }

    public void StartDmgCheck()
    {
        ctl.SetDmgCheck(true);
    }

    public void EndDmgCheck()
    {
        ctl.SetDmgCheck(false);
    }
}
