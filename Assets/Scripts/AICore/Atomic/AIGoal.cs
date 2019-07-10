using DefaultNamespace.Entitys;
using UnityEngine;

namespace DefaultNamespace.AICore
{
    public enum EAIGoalStatus
    {
        Inactive,
        Actived,
        Completed,
        Fail
    }

    public enum EAIGoalType
    {
        Idle,
        MoveToPos,
        Attack,
        Patrol,
        Rest,
        Think,
        Fight,
        SeekTarget,
        ActionAttack,
        Def,
        FightReady
    }
    
    public abstract class AIGoal
    {
        public RoleUnit_NPC owner;
        public EAIGoalStatus status;
        public EAIGoalType type;
        public float mStartTime;
        
        public AIGoal(RoleUnit_NPC pOwner)
        {
            owner = pOwner;
            status = EAIGoalStatus.Inactive;
            mStartTime = Time.time;
        }

        public abstract EAIGoalType GetType();
        
        //logic to run when the goal is activated.
        public virtual void Active()
        {
        }

        //logic to run each update-step
        public virtual EAIGoalStatus Process()
        {
//            Debug.Log("<Color=green>Process:" + GetType() + "," + status + "</color>");//###########
            return status;
        }

        //logic to run when the goal is satisfied. (typically used to switch
        //off any active steering behaviors)
        public virtual void Terminate()
        {
//            Debug.Log("<Color=red>Terminate:" + GetType() + "</color>");//###########
        }

        public virtual void AddSubGoal(AIGoal goal)
        {

        }

        public bool IsActive()
        {
            return status == EAIGoalStatus.Actived;
        }

        public bool IsInactive()
        {
            return status == EAIGoalStatus.Inactive;
        }

        public bool IsCompleted()
        {
            return status == EAIGoalStatus.Completed;
        }

        public bool HasFailed()
        {
            return status == EAIGoalStatus.Fail;
        }

        public void ActiveIfInactive()
        {
            if (IsInactive())
            {
               Active();
            }
        }

        public void ReactiveIfFail()
        {
            if (HasFailed())
            {
                status = EAIGoalStatus.Inactive;
            }
        }

        public virtual bool IsStuck()
        {
            return false;
        }
    }
}