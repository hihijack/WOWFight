using DefaultNamespace.Entitys;

namespace DefaultNamespace.AICore.GoalEvaluation
{
    public abstract class AIGoalEvaluator
    {
        public abstract float CalculateDesirability(RoleUnit_NPC owner);

        public abstract void SetGoal(AIGoal_Composite goalParent);

        public float CalDangerDes(RoleUnit_NPC owner)
        {
            float r = 0;
            RoleUnit target = owner.GetSensoryMemory().target;
            if (target != null)
            {
                if (owner.CheckDisIsNear(target, 2) && (target.CharaCtl.IsInState(EBSType.AtkBofere) || target.CharaCtl.IsInState(EBSType.Power)))
                {
                    r = 1;
                }
            }
            return r;
        }
        
    }
}