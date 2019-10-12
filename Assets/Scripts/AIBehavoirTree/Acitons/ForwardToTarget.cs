using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    /// <summary>
    /// 朝向当前目标
    /// </summary>
    [TaskCategory("MyAI")]
    [TaskDescription("朝向目标")]
    public class ForwardToTarget : Action
    {
        public SharedObject owner;
        private RoleUnit_NPC roleOwner;
                
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
        }

        public override TaskStatus OnUpdate()
        {
            if (roleOwner.IsFaceToTarget(roleOwner.GetSensoryMemory().target))
            {
                roleOwner.FaceToTarget(roleOwner.GetSensoryMemory().target);
            }

            return TaskStatus.Success;
        }
    }
}