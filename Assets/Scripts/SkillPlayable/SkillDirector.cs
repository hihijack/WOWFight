using System;
using DefaultNamespace.Entitys;
using DefaultNamespace.GameData;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.SkillPlayable
{
    public class SkillDirector
    {
        private CharacterCtl owner;

        public float time;

        private int frame;
        
        /// <summary>
        /// 行为结束。自然结束时会触发
        /// </summary>
        public Action onBehavAutoEnd;
        
        private SkillPlayBehaviour curBehav;
        private int skillID;

        public SkillDirector(CharacterCtl owner)
        {
            this.owner = owner;
        }

        public CharacterCtl GetOwner()
        {
            return owner;
        }
        
        public void CreateBehav(int skillID)
        {
           curBehav = new SkillPlayBehaviour(this);
           this.skillID = skillID;
            SKillDataNode skillData = GameDataMgr.Inst.skillTable.GetData(skillID);
            if (skillData.isValid())
            {
                foreach (var playableNodeData in skillData.playableDatas)
                {
                    SkillPlayBehavNodeBase node = null;
                    if (playableNodeData.type == EPlayableNodeType.Effect)
                    {
                        node = SkillPlayNode_CreateEff.CreateBehavNode(playableNodeData);
                    }
                    else if (playableNodeData.type == EPlayableNodeType.PlayAnim)
                    {
                       node = SkillPlayNode_PlayAnim.CreateBehavNode(playableNodeData);
                    }
                    else if (playableNodeData.type == EPlayableNodeType.Move)
                    {
                        node = SkillPlayNode_Move.CreateBehavNode(playableNodeData);
                    }
                    
                    AddBehavNode(node);
                }
            }
        }

        public void AddBehavNode(SkillPlayBehavNodeBase node)
        {
            curBehav.AddBehavNode(node);
        }
        
        public void Process()
        {
            if (curBehav != null)
            {
                time += Time.deltaTime;
                frame++;
                curBehav.ProcessNodes();
                if (curBehav.IsEnd())
                {
                    OnBehavEnd();
                    if (onBehavAutoEnd != null)
                    {
                        onBehavAutoEnd();
                    }
                }
            }
            else
            {
                time = 0;
                frame = 0;
            }
        }

        /// <summary>
        /// 中断
        /// </summary>
        public void Stop()
        {
            OnBehavEnd();
        }

        private void OnBehavEnd()
        {
            curBehav = null;
            time = 0;
            frame = 0;
        }

        public int Frame
        {
            get { return frame; }
        }

        public int SkillId
        {
            get { return skillID; }
        }

        /// <summary>
        /// 取当前帧的伤害盒信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SKillRect[] GetCurDmgBox()
        {
            if (SkillId > 0 && Frame > 0)
            {
                SKillDataNode data = GameDataMgr.Inst.skillTable.GetData(skillID);
                if (data.isValid())
                {
                    var skillBoxInfo = data.FindDmgBoxInfo(Frame);
                    if (skillBoxInfo.isValid())
                    {
                        return skillBoxInfo.rects;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 取当前身位盒信息
        /// </summary>
        /// <returns></returns>
        public SKillRect[] GetCurBodyBox()
        {
            if (SkillId > 0 && Frame > 0)
            {
                SKillDataNode data = GameDataMgr.Inst.skillTable.GetData(skillID);
                if (data.isValid())
                {
                    var skillBoxInfo = data.FindBodyBoxInfo(Frame);
                    if (skillBoxInfo.isValid())
                    {
                        return skillBoxInfo.rects;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 取当前碰撞盒信息
        /// </summary>
        /// <returns></returns>
        public SKillRect[] GetCurColliderBox()
        {
            if (SkillId > 0 && Frame > 0)
            {
                SKillDataNode data = GameDataMgr.Inst.skillTable.GetData(skillID);
                if (data.isValid())
                {
                    var skillBoxInfo = data.FindColliderBoxInfo(Frame);
                    if (skillBoxInfo.isValid())
                    {
                        return skillBoxInfo.rects;
                    }
                }
            }
            return null;
        }
    }
}