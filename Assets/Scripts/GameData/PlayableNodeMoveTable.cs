using System;
using UnityEngine;

namespace DefaultNamespace.GameData
{
    [CreateAssetMenu]
    public class PlayableNodeMoveTable : ScriptableObject
    {
        public PlayableNodeData_Move[] data;
        
        public PlayableNodeData_Move GetData(int id)
        {
            foreach (var t in data)
            {
                if (t.id == id)
                {
                    return t;
                }
            }

            return new PlayableNodeData_Move();
        }
    }

    [Serializable]
    public struct PlayableNodeData_Move
    {
        public int id;
        public Vector2 speed;

        public bool isValid()
        {
            return id > 0 && speed != Vector2.zero;
        }
    }
}