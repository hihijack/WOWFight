using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    /// <summary>
    /// 靠近目标
    /// </summary>
    public class AIGoal_SeekTarget : AIGoal
    {
        private float seekDis;
        private float struckTime;
        private RoleUnit target;
        
        public AIGoal_SeekTarget(RoleUnit_NPC pOwner, RoleUnit target, float seekDis) : base(pOwner)
        {
            this.seekDis = seekDis;
            struckTime = 8f;
            this.target = target;
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.SeekTarget;
        }

        public override void Active()
        {
            base.Active();
            status = EAIGoalStatus.Actived;
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
            
            if (IsStuck())
            {
                status = EAIGoalStatus.Fail;
            }
            else if (IsSeekTargetEnough())
            {
                status = EAIGoalStatus.Completed;
            }
            else
            {
                owner.CommandMoveTo(target.Pos, EAIGoalMoveType.Run, null);
            }
            
            return status;
        }

        private bool IsSeekTargetEnough()
        {
            return owner.CheckDisIsNear(target, seekDis);
        }

        public override bool IsStuck()
        {
            return Time.time - mStartTime > struckTime;
        }
    }
}