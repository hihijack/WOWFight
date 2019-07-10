using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace
{
    public class DebugMgr : MonoBehaviour
    {

        public RoleUnit_NPC targetAI;
        
        private static DebugMgr _inst;

        public static DebugMgr Inst
        {
            get { return _inst; }
        }

        private void Awake()
        {
            _inst = this;
        }
    }
}