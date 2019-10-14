using UI;
using UnityEngine;

namespace DefaultNamespace.Triger
{
    public class BaseTrigger : MonoBehaviour
    {
        private BoxCollider collider;
        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Actor") && other.name == "Player")
            {
               GameManager.Inst.curStayTrigger = this;
               OnEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Actor") && other.name == "Player")
            {
                GameManager.Inst.curStayTrigger = null;
                OnExit();
            }
        }

        protected virtual void OnEnter()
        {
            
        }
        
        protected virtual void OnExit()
        {
            
        }

        public virtual void OnActive()
        {
            collider.enabled = false;
            GameManager.Inst.OnSummonBoss();
        }
    }
}