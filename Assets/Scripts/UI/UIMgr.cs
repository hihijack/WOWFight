using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMgr : MonoBehaviour
    {
        public static UIMgr Inst;

        public Slider sldPlayerHP;
        public Text txtPlayerHP;
        
        private void Awake()
        {
            Inst = this;
        }

        private void Update()
        {
            RefreshPlayerInfo();
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
    }
}