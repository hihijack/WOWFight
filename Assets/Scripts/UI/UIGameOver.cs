using UnityEngine;

namespace UI
{
    public class UIGameOver : MonoBehaviour
    {
        private Animator anim;
        
        public GameObject goConTip;
        
        
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            anim.Play("Play",0, 0);
        }

        private void Update()
        {
            if (goConTip.activeInHierarchy && Input.anyKeyDown)
            {
                this.Close();
                GameManager.Inst.ReStartGame();
            }
        }

        private void Close()
        {
           gameObject.SetActive(false);
        }
    }
}