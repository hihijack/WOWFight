using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore.GoalEvaluation
{
    public class AIEvaluator_FightReady : AIGoalEvaluator
    {
        public override float CalculateDesirability(RoleUnit_NPC owner)
        {
           
            if (owner.GetSensoryMemory().target != null)
            {
                //（1 - 危险评估） * 随机[0.5,0.9]
                if (CalDangerDes(owner) > 0)
                {
                    return 0;
                }
                else
                {
                    return Random.Range(0.5f, 1f);
                }
            }
            else
            {
                return 0;
            }
        }

        public override void SetGoal(AIGoal_Composite goalParent)
        {
            goalParent.AddGoal_FightReady();
        }
    }
}