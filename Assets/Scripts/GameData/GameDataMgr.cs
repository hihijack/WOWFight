using UnityEngine;

namespace DefaultNamespace.GameData
{
    public class GameDataMgr : MonoBehaviour
    {

        public static GameDataMgr Inst;
        private void Awake()
        {
            Inst = this;
        }

        public SkillTable skillTable;
        public PlayableNodeAnimTable playableNodeAnimTable;
        public PlayableNodeEffectTable playableNodeEffectTable;
        public PlayableNodeMoveTable palyableNodeMoveTable;
    }
}