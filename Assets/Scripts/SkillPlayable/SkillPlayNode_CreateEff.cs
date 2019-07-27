using UnityEngine;

namespace DefaultNamespace.SkillPlayable
{  
    public struct SkillPlayData_Eff
    {
        public string eff;
        public string point;//bind;ori;@bone
        public Vector3 offset;
    }
    
    public class SkillPlayNode_CreateEff : SkillPlayBehavNodeBase
    {
        public SkillPlayData_Eff effData;
        
        public override void OnPlay()
        {
            EffectUtil.CreateEffWithData(behavour.GetDirector().GetOwner().RoleUnit, effData);
        }

        public override void OnProcess()
        {
        }

        public override void OnExit()
        {
        }
    }
}