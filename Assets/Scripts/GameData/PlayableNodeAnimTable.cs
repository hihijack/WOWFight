using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace DefaultNamespace.GameData
{
    [CreateAssetMenu]
    public class PlayableNodeAnimTable : ScriptableObject
    {
        public PlayableNodeData_Anim[] data;
        
        public PlayableNodeData_Anim GetData(int id)
        {
            foreach (var t in data)
            {
                if (t.id == id)
                {
                    return t;
                }
            }

            return new PlayableNodeData_Anim();
        }
    }

    [Serializable]
    public struct PlayableNodeData_Anim
    {
        public int id;
        public string anim;

        public bool isValid()
        {
            return id > 0 && !string.IsNullOrEmpty(anim);
        }
    }
}