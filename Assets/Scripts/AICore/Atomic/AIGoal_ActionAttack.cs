using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    /// <summary>
    /// 攻击动作
    /// </summary>
    public class AIGoal_ActionAttack : AIGoal
    {
        public AIGoal_ActionAttack(RoleUnit_NPC pOwner) : base(pOwner)
        {
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.ActionAttack;
        }

        public override void Active()
        {
            base.Active();
            status = EAIGoalStatus.Actived;
        }

        public override EAIGoalStatus Process()
        {
            base.Process();
            ActiveIfInactive();
            if (owner.CharaCtl.IsInState(EBSType.AtkBofere))
            {
                status = EAIGoalStatus.Completed;
            }
            else if (IsStuck())
            {
                status = EAIGoalStatus.Fail;
            }
            else
            {
                owner.CommandAttack();
            }
            return status;
        }

        public override bool IsStuck()
        {
            return Time.time - mStartTime > 3f;
        }
    }
}