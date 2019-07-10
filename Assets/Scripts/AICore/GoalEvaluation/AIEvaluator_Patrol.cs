using DefaultNamespace.Entitys;

namespace DefaultNamespace.AICore.GoalEvaluation
{
    public class AIEvaluator_Patrol : AIGoalEvaluator
    {
        public override float CalculateDesirability(RoleUnit_NPC owner)
        {
            if (owner.GetSensoryMemory().target != null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public override void SetGoal(AIGoal_Composite goalParent)
        {
            goalParent.AddGoal_Patrol();
        }
    }
}