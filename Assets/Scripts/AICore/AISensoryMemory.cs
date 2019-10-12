using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public class AISensoryMemory
    {
        public RoleUnit_NPC owner;

        public RoleUnit target;
        
        public AISensoryMemory(RoleUnit_NPC pOwner)
        {
            owner = pOwner;
        }

        public void UpdateVision()
        {
            if (GameManager.Inst.targetRole.alive)
            {
                if ((GameManager.Inst.targetRole.Pos - owner.Pos).sqrMagnitude <= 64)
                {
                    target = GameManager.Inst.targetRole;
                }
            }
            else
            {
                target = null;
            }
        }
    }
}