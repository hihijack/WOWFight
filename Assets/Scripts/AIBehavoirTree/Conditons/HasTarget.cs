using AIBehavoirTree.CustomSharedVal;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("是否有目标")]
    public class HasTarget : Conditional
    {
        public SharedObject owner;

        public override TaskStatus OnUpdate()
        {
            if (owner == null)
            {
                return TaskStatus.Failure;
            }

            var roleOwner = owner.Value as RoleUnit_NPC;
            
            if (roleOwner.GetSensoryMemory().target != null)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}