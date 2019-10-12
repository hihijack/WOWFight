using UnityEngine;

namespace DefaultNamespace.SkillPlayable
{
    public enum EPlayableNodeProcessState
    {
        Inactive, //未开始
        Processing, //正在进行
        End //结束
    }
    
    public abstract class SkillPlayBehavNodeBase
    {
        public float timeStart;
        public float timeDur;
        private EPlayableNodeProcessState state = EPlayableNodeProcessState.Inactive;
        public abstract void OnPlay();
        public abstract void OnProcess();
        public abstract void OnExit();
        
        public SkillPlayBehaviour behavour;
        
        public EPlayableNodeProcessState Process(float directorTime)
        {
            if (directorTime < timeStart)
            {
                state = EPlayableNodeProcessState.Inactive;
            }
            else if (directorTime > timeStart + timeDur)
            {
                if (state == EPlayableNodeProcessState.Processing)
                {
                    OnExit();
                }
                state = EPlayableNodeProcessState.End;
            }
            else
            {
                if (state == EPlayableNodeProcessState.Inactive)
                {
                    OnPlay();
                }
                state = EPlayableNodeProcessState.Processing;
                OnProcess();
            }

            return state;
        }

        public EPlayableNodeProcessState GetState()
        {
            return state;
        }
    }
}