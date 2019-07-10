using DefaultNamespace.Entitys;

namespace DefaultNamespace.AICore
{
    public class AIGoal_Attack : AIGoal_Composite
    {
        public AIGoal_Attack(RoleUnit_NPC pOwner) : base(pOwner)
        {
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.Attack;
        }

        public override void Active()
        {
            base.Active();
            status = EAIGoalStatus.Actived;
            var target = owner.GetSensoryMemory().target;
            if (target != null && target.alive)
            {
                AddSubGoal(new AIGoal_ActionAttack(owner));
                AddSubGoal(new AIGoal_SeekTarget(owner, owner.GetSensoryMemory().target, 1f));
            }
            else
            {
                status = EAIGoalStatus.Completed;
            }
        }

        public override EAIGoalStatus Process()
        {
            base.Process();
            ActiveIfInactive();
            status = ProcessSubGoals();
            return status;
        }
    }
}