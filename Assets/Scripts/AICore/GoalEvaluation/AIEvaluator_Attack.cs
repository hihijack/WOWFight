using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore.GoalEvaluation
{
    public class AIEvaluator_Attack : AIGoalEvaluator
    {
        public override float CalculateDesirability(RoleUnit_NPC owner)
        {
            if (owner.GetSensoryMemory().target != null)
            {
                //（1 - 危险评估）* 随机[.1-.9]
                return (1 - CalDangerDes(owner)) * Random.Range(0.6f, 0.9f);
            }
            else
            {
                return 0;
            }
        }

        public override void SetGoal(AIGoal_Composite goalParent)
        {
            goalParent.AddGoal_Attack();
        }
    }
}