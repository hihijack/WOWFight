using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.AICore;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("移动到点")]
    public class MoveToPoint : Action
    {
        public SharedObject owner;
        public SharedObject target;
        
        public SharedVector3 moveTargetPoint;

        public EAIGoalMoveType moveType;

        [Header("保持朝向目标")]
        public bool keepFaceToTarget;
        
        private RoleUnit_NPC roleOwner;
        private RoleUnit roleTarget;
        
        private float timeToReachPos;

        private float startTime;
        
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
            if (target != null)
            {
                roleTarget = target.Value as RoleUnit;
            }
            startTime = Time.time;
            timeToReachPos = CalTimeToReachPos(moveTargetPoint.Value, moveType);
            timeToReachPos += 1;
        }

        public override TaskStatus OnUpdate()
        {
            if (!roleOwner.alive)
            {
                return TaskStatus.Failure;
            }
            
            roleOwner.CommandMoveTo(moveTargetPoint.Value, moveType, keepFaceToTarget ? roleTarget : null);
            if (IsStuck())
            {
                roleOwner.CommandStopRun();
                return TaskStatus.Failure;
            }
            else if(roleOwner.IsAtPosition(moveTargetPoint.Value))
            {
                roleOwner.CommandStopRun();
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            roleOwner.CommandStopRun();
        }

        private float CalTimeToReachPos(Vector3 targetPos, EAIGoalMoveType moveType)
        {
            return (roleOwner.Pos - targetPos).magnitude / roleOwner.GetMoveSpeed(moveType);
        }
        
        public bool IsStuck()
        {
            return Time.time - startTime > timeToReachPos;
        }
    }
}