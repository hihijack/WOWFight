using DefaultNamespace.Entitys;

namespace DefaultNamespace.AICore.GoalEvaluation
{
    public class AIEvaluator_Fight : AIGoalEvaluator
    {
        public override float CalculateDesirability(RoleUnit_NPC owner)
        {
            if (owner.GetSensoryMemory().target != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override void SetGoal(AIGoal_Composite goalParent)
        {
            goalParent.AddGoal_Fight();
        }
    }
}