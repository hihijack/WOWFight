using System.Collections.Generic;

namespace DefaultNamespace.SkillPlayable
{
    public class SkillPlayBehaviour
    {
        private List<SkillPlayBehavNodeBase> behavNodes;
        private SkillDirector director;
        public SkillPlayBehaviour(SkillDirector director)
        {
            this.director = director;
            behavNodes = new List<SkillPlayBehavNodeBase>();
        }

        public void AddBehavNode(SkillPlayBehavNodeBase node)
        {
            this.behavNodes.Add(node);
            node.behavour = this;
        }
        
        public void ProcessNodes()
        {
            foreach (var behavNode in behavNodes)
            {
               behavNode.Process(director.time);
            }
        }

        public SkillDirector GetDirector()
        {
            return director;
        }
        
        /// <summary>
        /// 行为是否结束
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            bool r = true;
            foreach (var node in behavNodes)
            {
                if (node.GetState() != EPlayableNodeProcessState.End)
                {
                    r = false;
                }
            }
            return r;
        }
    }
}