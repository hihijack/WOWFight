using System.Collections.Generic;
using System.Linq;
using System.Text;
using DefaultNamespace.AICore.GoalEvaluation;
using DefaultNamespace.Entitys;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace DefaultNamespace.AICore
{
    public class AIGoal_Think : AIGoal_Composite
    {
        
        public AIGoal_Think(RoleUnit_NPC pOwner) : base(pOwner)
        {
            evaluators = new List<AIGoalEvaluator>();
            evaluators.Add(new AIEvaluator_Patrol());
            evaluators.Add(new AIEvaluator_Fight());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Think:");
            foreach (var subGoal in subGoalList)
            {
                sb.AppendLine(subGoal.ToString());
            }
            return sb.ToString();
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.Think;
        }

        public override void Active()
        {
            base.Active();
            Arbitare();
            status = EAIGoalStatus.Actived;
        }

        public override EAIGoalStatus Process()
        {
            base.Process();
            ActiveIfInactive();
            EAIGoalStatus subGoalStatus = ProcessSubGoals();
            //当目标完成或失败，重新评估目标
            if (subGoalStatus == EAIGoalStatus.Completed || subGoalStatus == EAIGoalStatus.Fail)
            {
                status = EAIGoalStatus.Inactive;
            }

            return status;
        }
        
        
        public override void AddGoal_Patrol()
        {
            if (isNotPresent(EAIGoalType.Patrol))
            {
                RemoveAllSubGoals();
                AddSubGoal(new AIGoal_Patrol(owner, owner.GetPatrolPos()));             
            }
        }

        public override void AddGoal_Attack()
        {
            if (isNotPresent(EAIGoalType.Attack))
            {
                RemoveAllSubGoals();
                AddSubGoal(new AIGoal_Attack(owner));
            }
        }

        public override void AddGoal_Fight()
        {
            if (isNotPresent(EAIGoalType.Fight))
            {
                RemoveAllSubGoals();
                AddSubGoal(new AIGoal_Fight(owner));
            }
        }
    }
}