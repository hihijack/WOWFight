using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMgr : MonoBehaviour
    {
        public static UIMgr Inst;

        public Slider sldPlayerHP;
        public Text txtPlayerHP;

        public GameObject goTarget;
        public Slider sldTargetHP;
        public Text txtTargetHP;

        public UIGameOver uiGameOver;
        public UIGameWin uiGameWin;
        public GameObject goUIControlTip;
        public GameObject goUISummonTip;
        
        private void Awake()
        {
            Inst = this;
        }

        private void Update()
        {
            RefreshPlayerInfo();
            RefreshTargetInfo();
        }

        public void RefreshPlayerInfo()
        {
            var data = GameManager.Inst.targetRole.GetInfoData();
            if (data != null)
            {
                sldPlayerHP.value = (float)data.hpCur / data.hpMax;
                txtPlayerHP.text = string.Format("{0}/{1}", data.hpCur, data.hpMax);
            }
        }

        void RefreshTargetInfo()
        {
            var targetRole = GameManager.Inst.targetRole.atkTarget;
            if (targetRole != null && targetRole.alive)
            {
                goTarget.SetActive(true);
                var data = targetRole.GetInfoData();
                if (data != null)
                {
                    sldTargetHP.value = (float)data.hpCur / data.hpMax;
                    txtTargetHP.text = string.Format("{0}/{1}", data.hpCur, data.hpMax);
                }
            }
            else
            {
                goTarget.SetActive(false);
            }
        }

        public void ShowUIGameOver()
        {
            uiGameOver.Show();
        }

        public void ShowUIGameWin()
        {
            uiGameWin.Show();
        }

        public void TogUIControlTip(bool visible)
        {
            goUIControlTip.active = visible;
        }

        public void TogUISummonTip(bool visible)
        {
            goUISummonTip.active = visible;
        }
    }
}