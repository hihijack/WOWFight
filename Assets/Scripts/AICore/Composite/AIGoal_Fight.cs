using System.Collections.Generic;
using DefaultNamespace.AICore.GoalEvaluation;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public class AIGoal_Fight : AIGoal_Composite
    {
     
        public AIGoal_Fight(RoleUnit_NPC pOwner) : base(pOwner)
        {
            evaluators = new List<AIGoalEvaluator>
            {
                new AIEvaluator_Attack(),
                new AIEvaluator_Def(),
                new AIEvaluator_FightReady()
            };
        }

        public override void Active()
        {
            base.Active();
            status = EAIGoalStatus.Actived;
            RemoveAllSubGoals();
            Arbitare();
        }

        public override EAIGoalStatus Process()
        {
            base.Process();
            ActiveIfInactive();
            
            //战斗策略
            if (Time.frameCount % 60 == 0)
            {
                Arbitare();
            }
            
            status = ProcessSubGoals();
            return status;
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.Fight;
        }

        public override void AddGoal_Attack()
        {
            if (isNotPresent(EAIGoalType.Attack))
            {
                RemoveAllSubGoals();
                AddSubGoal(new AIGoal_Attack(owner));
            }
        }

        public override void AddGoal_Def()
        {
            if (isNotPresent(EAIGoalType.Def))
            {
                RemoveAllSubGoals();
                AddSubGoal(new AIGoal_ActionDef(owner));
            }
        }

        public override void AddGoal_FightReady()
        {
            if (isNotPresent(EAIGoalType.FightReady))
            {
                RemoveAllSubGoals();
                AddSubGoal(new AIGoal_FightReady(owner,owner.GetSensoryMemory().target, 2, 6));
            }
        }
    }
}