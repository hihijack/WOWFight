using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.AICore.GoalEvaluation;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public abstract class AIGoal_Composite : AIGoal
    {
        protected List<AIGoal> subGoalList;
        protected List<AIGoalEvaluator> evaluators;
        protected AIGoal_Composite(RoleUnit_NPC pOwner) : base(pOwner)
        {
            subGoalList = new List<AIGoal>();
        }
        
        protected EAIGoalStatus ProcessSubGoals()
        {
            EAIGoalStatus subGoalStatus = EAIGoalStatus.Inactive;
           
            while (subGoalList.Count > 0 && (subGoalList.Last().IsCompleted() || subGoalList.Last().HasFailed()))
            {
                AIGoal lastGoal = subGoalList.Last();
                subGoalList.Last().Terminate();
                subGoalList.Remove(lastGoal);
            }

            if (subGoalList.Count > 0)
            {
                var lastGoal = subGoalList.Last();
                subGoalStatus = lastGoal.Process();
                if (subGoalStatus == EAIGoalStatus.Completed && subGoalList.Count > 1)
                {
                    return EAIGoalStatus.Actived;
                }
                return subGoalStatus;
            }
            else
            {
                return EAIGoalStatus.Completed;
            }           
        }


        public override void AddSubGoal(AIGoal goal)
        {
            subGoalList.Add(goal);
        }

        protected void RemoveAllSubGoals()
        {
            foreach (var goal in subGoalList)
            {
                goal.Terminate();
            }
            subGoalList.Clear();
        }


        public List<AIGoal> GetSubGoals()
        {
            return subGoalList;
        }
        
        public void Arbitare()
        {
            float best = 0;
            AIGoalEvaluator bestEvaluator = null;
            foreach (var evaluator in evaluators)
            {
                float des = evaluator.CalculateDesirability(owner);
                if (des >= best)
                {
                    best = des;
                    bestEvaluator = evaluator;
                }
            }

            Debug.Assert(bestEvaluator != null, "bestEvaluator != null");
            bestEvaluator.SetGoal(this);
        }

        public bool isNotPresent(EAIGoalType goalType)
        {
            if (subGoalList.Count > 0)
            {
                return subGoalList.Last().GetType() != goalType;
            }

            return true;
        }
        
        public virtual void AddGoal_Attack(){}
        public virtual void AddGoal_Def(){}
        public virtual void AddGoal_Fight(){}
        public virtual void AddGoal_FightReady(){}
        public virtual void AddGoal_Patrol(){}
    }
}