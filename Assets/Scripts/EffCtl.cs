using UnityEngine;

namespace DefaultNamespace
{
    public class EffCtl : MonoBehaviour
    {
        private ParticleSystem[] pss;

        private EffLifeTimer timer;
        
        private void Awake()
        {
            pss = GetComponentsInChildren<ParticleSystem>();
            timer = GetComponent<EffLifeTimer>();
        }

        public void Play()
        {
            foreach (var ps in pss)
            {
                ps.Play();
            }
   
            if (timer)
            {
                //使用预制配置时间
                timer.StartLife(); 
            }  
        }
    }
}