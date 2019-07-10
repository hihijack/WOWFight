using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public class AIGoal_FightReady : AIGoal_Composite
    {
        private float near;
        private float far;
        private RoleUnit target;

        /// <summary>
        /// 过近时后跳，然后保持一定距离
        /// </summary>
        /// <param name="pOwner"></param>
        public AIGoal_FightReady(RoleUnit_NPC pOwner, RoleUnit target, float near, float far) : base(pOwner)
        {
            this.near = near;
            this.far = far;
            this.target = target;
        }

        public override EAIGoalType GetType()
        {
            return EAIGoalType.FightReady;
        }

        public override void Active()
        {
            base.Active();
            status = EAIGoalStatus.Actived;

            var dir = (owner.Pos - target.Pos).normalized;
            Vector3 pointA = target.Pos + dir * near;
            Vector3 pointB = target.Pos + dir * far;
            
            //行走移动到区间内随机位置，保持朝向
            Vector3 posTarget = Vector3.Lerp(pointA, pointB, Random.Range(0f, 1f));
            AddSubGoal(new AIGoal_MoveToPos(owner, target, posTarget, EAIGoalMoveType.Walk));
        }

        /// <summary>
        /// 计算行走进入舒服区的时间
        /// </summary>
        /// <returns></returns>
        private float CalTimeToInto(Vector3 pointA, Vector3 pointB)
        {
            return owner.CalDisToRange(pointA, pointB) / owner.GetMoveSpeed(EAIGoalMoveType.Walk);
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