using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("是否在状态中")]
    public class IsInState : Conditional
    {
        public SharedObject role;
        public EBSType state;

        private RoleUnit_NPC roleUnit;
        
        
        public override void OnStart()
        {
            roleUnit = role.Value as RoleUnit_NPC;
        }
        
        public override TaskStatus OnUpdate()
        {
            if (role.Value == null)
            {
                return TaskStatus.Failure;
            }
            
            if (roleUnit.CharaCtl.GetFSM().CurState.type == state)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}