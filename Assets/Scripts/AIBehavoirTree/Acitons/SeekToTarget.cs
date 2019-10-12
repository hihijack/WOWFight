using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.AICore;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("追逐目标")]
    public class SeekToTarget : Action
    {
        public SharedObject owner;
        public SharedObject taret;
        public EAIGoalMoveType moveType;
        public float minDis;
        private RoleUnit_NPC roleOwner;
        private RoleUnit roleTarget;
        
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
            roleTarget = taret.Value as RoleUnit;
        }

        public override TaskStatus OnUpdate()
        {
            if (!roleTarget.alive || !roleOwner.alive)
            {
                return TaskStatus.Failure;
            }
            
            if (roleOwner.CheckDisIsNear(roleTarget, minDis))
            {
                roleOwner.CommandStopRun();
                return TaskStatus.Success;
            }
            else
            {
                //靠近目标
                roleOwner.CommandMoveTo(roleTarget.Pos, moveType, null);
                return TaskStatus.Running;
            }
        }
    }
}