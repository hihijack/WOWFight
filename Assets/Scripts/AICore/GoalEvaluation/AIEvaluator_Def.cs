using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore.GoalEvaluation
{
    public class AIEvaluator_Def : AIGoalEvaluator
    {
        public override float CalculateDesirability(RoleUnit_NPC owner)
        {
             if (owner.GetSensoryMemory().target != null)
            {
                // 危险评估 * 随机[0.5,0.9]
                return CalDangerDes(owner) * Random.Range(0.7f, 1f);
            }
             else
             {
                 return 0;
             }
        }

        public override void SetGoal(AIGoal_Composite goalParent)
        {
            goalParent.AddGoal_Def();
        }
    }
}