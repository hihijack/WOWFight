using UI;
using UnityEngine;

namespace DefaultNamespace.Triger
{
    public class TrigerSummonBoss : BaseTrigger
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            UIMgr.Inst.TogUIControlTip(false);
            UIMgr.Inst.TogUISummonTip(true);
        }

        protected override void OnExit()
        {
            base.OnExit();
            UIMgr.Inst.TogUIControlTip(true);
            UIMgr.Inst.TogUISummonTip(false);
        }

        public override void OnActive()
        {
            base.OnActive();
        }
    }
}