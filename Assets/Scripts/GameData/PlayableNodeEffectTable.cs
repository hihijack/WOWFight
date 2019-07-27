using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DefaultNamespace.GameData
{
    [CreateAssetMenu]
    public class PlayableNodeEffectTable : ScriptableObject
    {
        [TableList(ShowIndexLabels = false)]
        public PlayableNodeData_Effect[] data;
        
        public PlayableNodeData_Effect GetData(int id)
        {
            foreach (var t in data)
            {
                if (t.id == id)
                {
                    return t;
                }
            }

            return new PlayableNodeData_Effect();
        }
    }

    [Serializable]
    public struct PlayableNodeData_Effect
    {
        public int id;
        public string eff;
        public string point;
        public Vector3 offset;
        
        public bool isValid()
        {
            return id > 0;
        }
    }
}