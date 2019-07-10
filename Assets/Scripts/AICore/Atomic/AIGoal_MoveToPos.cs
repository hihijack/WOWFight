using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public enum EAIGoalMoveType
    {
        Walk,
        Run,
        Rush,
        Roll
    }
    
    /// <summary>
    /// 以指定方式移动到目标点
    /// </summary>
    public class AIGoal_MoveToPos : AIGoal
    {
        private Vector3 targetPos;
        private EAIGoalMoveType moveType;
        private float timeToReachPos;
        private RoleUnit keepFaceToTarget;//行走时保持朝向目标
        public AIGoal_MoveToPos(RoleUnit_NPC pOwner, RoleUnit keepFaceToTarget, Vector3 targetPos, EAIGoalMoveType moveType) : base(pOwner)
        {
            this.targetPos = targetPos;
            this.moveType = moveType;
            this.keepFaceToTarget = keepFaceToTarget;
            timeToReachPos = 0;
        }

        public override string ToString()
        {
            return "MoveToPos-" + targetPos + "," + moveType;
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.MoveToPos;
        }

        public override void Active()
        {
            base.Active();
            status = EAIGoalStatus.Actived;
            timeToReachPos = CalTimeToReachPos(targetPos, moveType);
            timeToReachPos += 1;
            
        }

        private float CalTimeToReachPos(Vector3 targetPos, EAIGoalMoveType moveType)
        {
            return (owner.Pos - targetPos).magnitude / owner.GetMoveSpeed(moveType);
        }

        public override void Terminate()
        {
            base.Terminate();
            status = EAIGoalStatus.Completed;
        }

        public override EAIGoalStatus Process()
        {
            base.Process();
            ActiveIfInactive();

            owner.CommandMoveTo(targetPos, moveType, keepFaceToTarget);
            
            if (IsStuck())
            {
                status = EAIGoalStatus.Fail;
            }
            else if(owner.IsAtPosition(targetPos))
            {
                status = EAIGoalStatus.Completed;
            }
            return status;
        }

        public override bool IsStuck()
        {
            return Time.time - mStartTime > timeToReachPos;
        }
    }
}