using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public class AIGoal_Patrol : AIGoal_Composite
    {
        private Vector3[] points;
        public AIGoal_Patrol(RoleUnit_NPC pOwner, Vector3[] points) : base(pOwner)
        {
            this.points = points;
        }

        public override string ToString()
        {
            return "Patrol";
        }

        public override void Active()
        {
            base.Active();
            
            status = EAIGoalStatus.Actived;
            AddSubGoal(new AIGoal_MoveToPos(owner, null, points[0], EAIGoalMoveType.Walk));
            AddSubGoal(new AIGoal_MoveToPos(owner, null, points[1], EAIGoalMoveType.Walk));
        }

        public override EAIGoalStatus Process()
        {
            base.Process();
            
            ActiveIfInactive();

            status = ProcessSubGoals();

            return status;
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.Patrol;
        }
    }
}