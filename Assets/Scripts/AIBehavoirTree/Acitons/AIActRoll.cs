using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;

namespace DefaultNamespace.AIBehavoirTree
{
    /// <summary>
    /// 翻滚方向
    /// </summary>
    public enum EAIRollDir
    {
        Forward, //前
        Back    //后
    }
    
    [TaskCategory("MyAI")]
    [TaskDescription("翻滚")]
    public class AIActRoll : Action
    {
        public SharedObject owner;
        public EAIRollDir dir;

        public override TaskStatus OnUpdate()
        {
            RoleUnit_NPC roleOwner = owner.Value as RoleUnit_NPC;
            if (!roleOwner.alive)
            {
                return TaskStatus.Failure;
            }

            roleOwner.CommandRoll(dir);
            return TaskStatus.Success;
        }
    }
}