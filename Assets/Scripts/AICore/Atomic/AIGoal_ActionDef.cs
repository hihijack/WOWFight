using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public class AIGoal_ActionDef : AIGoal
    {
        public AIGoal_ActionDef(RoleUnit_NPC pOwner) : base(pOwner)
        {
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
            if (owner.CharaCtl.IsInState(EBSType.Parry))
            {
                status = EAIGoalStatus.Completed;
            }
            else if (IsStuck())
            {
                status = EAIGoalStatus.Fail;
            }
            else
            {
                owner.CommandParry();
            }
            return status;
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.Def;
        }

        public override bool IsStuck()
        {
            return Time.time - mStartTime > Time.deltaTime * 2;
        }
    }
}