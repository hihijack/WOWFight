using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("设置战斗准备目标移动点")]
    public class AIActSetFightReadyPoint : Action
    {
        public SharedObject target;
        public SharedObject owner;
        public SharedFloat near;
        public SharedFloat far;
        public SharedVector3 resultStore;
        
        private RoleUnit_NPC roleOwner;
        private RoleUnit roleTarget;
        
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
            roleTarget = target.Value as RoleUnit;
        }

        public override TaskStatus OnUpdate()
        {
            if (!roleTarget.alive)
            {
                return TaskStatus.Failure;
            }
            var dir = (roleOwner.Pos - roleTarget.Pos).normalized;
            var pointA = roleTarget.Pos + dir * near.Value;
            var pointB = roleTarget.Pos + dir * far.Value;
            var posTarget = Vector3.Lerp(pointA, pointB, Random.Range(0f, 1f));
            resultStore.Value = posTarget;
            return TaskStatus.Success;
        }
    }
}