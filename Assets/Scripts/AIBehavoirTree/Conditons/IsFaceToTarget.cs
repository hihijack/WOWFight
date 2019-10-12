using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;
using Unity.Collections;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("是否朝向目标")]
    public class IsFaceToTarget : Conditional
    {
        public SharedObject owner;
        public SharedObject target;
        private RoleUnit roleTaget;
        private RoleUnit roleOwner;
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit;
            roleTaget = target.Value as RoleUnit;
        }

        public override TaskStatus OnUpdate()
        {
            if (roleOwner.IsFaceToTarget(roleTaget))
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