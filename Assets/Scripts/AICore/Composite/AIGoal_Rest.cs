using DefaultNamespace.Entitys;

namespace DefaultNamespace.AICore
{
    public class AIGoal_Rest : AIGoal_Composite
    {
        public AIGoal_Rest(RoleUnit_NPC pOwner) : base(pOwner)
        {
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.Rest;
        }
    }
}