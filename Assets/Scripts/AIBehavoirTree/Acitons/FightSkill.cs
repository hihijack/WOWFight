using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("释放技能")]
    public class FightSkill : Action
    {
        public SharedObject owner;
        public int skillID;
        private RoleUnit_NPC roleOwner;
        private float mStartTime;
        
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
            mStartTime = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            if (!roleOwner.alive)
            {
                return TaskStatus.Failure;
            }
            
            if (roleOwner.CharaCtl.IsInState(EBSType.SKill))
            {
                return TaskStatus.Success;
            }
            else if (IsStuck())
            {
                return TaskStatus.Failure;
            }
            else
            {
                roleOwner.CommandAttack(skillID);
            }
            return TaskStatus.Running;
        }
        
        private bool IsStuck()
        {
            return Time.time - mStartTime > Time.deltaTime * 2;
        }
    }
}