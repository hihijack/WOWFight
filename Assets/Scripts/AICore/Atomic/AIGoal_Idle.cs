using DefaultNamespace.Entitys;

namespace DefaultNamespace.AICore
{
    public class AIGoal_Idle : AIGoal
    {
        public AIGoal_Idle(RoleUnit_NPC pOwner) : base(pOwner)
        {
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.Idle;
        }
    }
}