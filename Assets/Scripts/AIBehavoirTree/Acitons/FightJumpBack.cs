using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("后跳")]
    public class FightJumpBack : Action
    {
        public SharedObject owner;
        private RoleUnit_NPC roleOwner;
        
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
        }

        public override TaskStatus OnUpdate()
        {
            roleOwner.CommandJumpBack();
            return TaskStatus.Success;
        }
    }
}