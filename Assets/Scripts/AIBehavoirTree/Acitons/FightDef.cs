using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DefaultNamespace.AICore;
using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    /// <summary>
    /// 战斗 - 防御
    /// </summary>
    [TaskCategory("MyAI")]
    [TaskDescription("防御")]
    public class FightDef : Action
    {
        public SharedObject owner;
        private RoleUnit_NPC roleOwner;
        private float mStartTime;
        public override void OnStart()
        {
            roleOwner = owner.Value as RoleUnit_NPC;
            mStartTime = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            if (roleOwner.CharaCtl.IsInState(EBSType.Parry))
            {
                return TaskStatus.Success;
            }
            else if (IsStuck())
            {
                return TaskStatus.Failure;
            }
            else
            {
                roleOwner.CommandParry();
            }
            return TaskStatus.Running;
        }
        
        private bool IsStuck()
        {
            return Time.time - mStartTime > Time.deltaTime * 2;
        }
    }
}