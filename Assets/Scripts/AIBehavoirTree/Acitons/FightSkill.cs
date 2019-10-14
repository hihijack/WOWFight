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
        
        private float stuckDur;
        private bool hasToState;
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
            stuckDur = 0;
            hasToState = false;
            roleOwner.CommandAttack(skillID); 
        }

        public override TaskStatus OnUpdate()
        {
//            if (!roleOwner.alive)
//            {
//                return TaskStatus.Failure;
//            }
//            
//            if (roleOwner.CharaCtl.IsInState(EBSType.SKill))
//            {
//                return TaskStatus.Success;
//            }
//            else
//            {
//                mStuckDur += Time.deltaTime;
//                if (mStuckDur > 0.1f)
//                {
//                    return TaskStatus.Failure;
//                }
//                else
//                {
//                    roleOwner.CommandAttack(skillID); 
//                    return TaskStatus.Running;
//                }
//            }
            if (!roleOwner.alive)
            {
                return TaskStatus.Failure;
            }


            if (!roleOwner.CharaCtl.IsInState(EBSType.SKill))
            {
                stuckDur += Time.deltaTime;
                if (stuckDur >= 0.1f)
                {
                    return TaskStatus.Failure;
                }
            }

            if (hasToState && !roleOwner.CharaCtl.IsInState(EBSType.SKill))
            {
                return TaskStatus.Success;
            }
            else
            {
                if (roleOwner.CharaCtl.IsInState(EBSType.SKill))
                {
                    hasToState = true;
                }
                return TaskStatus.Running;
            }
        }
    }
}