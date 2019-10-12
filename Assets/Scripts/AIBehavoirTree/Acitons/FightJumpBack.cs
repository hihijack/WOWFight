using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("后跳")]
    public class FightJumpBack : Action
    {
        public SharedObject owner;
        private RoleUnit_NPC roleOwner;

        private bool hasToJumpState;

        private float stuckDur;
        
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
            hasToJumpState = false;
            stuckDur = 0;
            roleOwner.CommandJumpBack();
        }

        public override TaskStatus OnUpdate()
        {
            if (!roleOwner.alive)
            {
                return TaskStatus.Failure;
            }


            if (!roleOwner.CharaCtl.IsInState(EBSType.JumpBack))
            {
                stuckDur += Time.deltaTime;
                if (stuckDur >= 0.1f)
                {
                    return TaskStatus.Failure;
                }
            }

            if (hasToJumpState && !roleOwner.CharaCtl.IsInState(EBSType.JumpBack))
            {
                return TaskStatus.Success;
            }
            else
            {
                if (roleOwner.CharaCtl.IsInState(EBSType.JumpBack))
                {
                    hasToJumpState = true;
                }
                return TaskStatus.Running;
            }
        }
    }
}