using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace DefaultNamespace.AIBehavoirTree
{
    [TaskCategory("MyAI")]
    [TaskDescription("当前时间与指定变量的差值是否大于一个随机值")]
    public class TimeDiff : Conditional
    {
        public SharedFloat time0;
        public SharedFloat ranValMin;
        public SharedFloat ranValMax;

        public override TaskStatus OnUpdate()
        {
            if (Time.time - time0.Value >= Random.Range(ranValMin.Value, ranValMax.Value))
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