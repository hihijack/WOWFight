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
        public SharedRoleUnitNPC owner;

        public override TaskStatus OnUpdate()
        {
            if (owner == null)
            {
                return TaskStatus.Failure;
            }
            
            if (owner.Value.GetSensoryMemory().target != null)
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