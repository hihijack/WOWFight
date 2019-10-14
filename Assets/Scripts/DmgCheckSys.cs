using System.Collections.Generic;
using Cinemachine;
using DefaultNamespace.GameData;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// 伤害判定开关；检测伤害判定结果目标
    /// </summary>
    public class DmgCheckSys
    {
        private CharacterCtl _charaCtl;
        private List<CharacterCtl> _extends; //判断除外列表.受击一次后加入列表.每次开启重置

        private bool _opening;
        
        public DmgCheckSys(CharacterCtl ctl)
        {
            _charaCtl = ctl;
            _extends = new List<CharacterCtl>(10);
        }
        
        
        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            //从表中取盒信息
            SKillRect[] dmgBoxs = _charaCtl.GetDmgRects();
            if (dmgBoxs != null && dmgBoxs.Length > 0)
            {
                if (!_opening)
                {
                    OnOpenDmgBox();
                }
                _opening = true;
            }
            else
            {
                _opening = false;
            }
            
            if (!_opening) return;
                
            if (_charaCtl.RoleUnit._data.camp == ECamp.Allies)
            {
                //遍历所有敌人
                foreach (var roleT in GameManager.Inst.aiCtls)
                {
                    if (!roleT.gameObject.activeInHierarchy || roleT.CharaCtl == _charaCtl || !roleT.CharaCtl.hurtedEnable || _extends.Contains(roleT.CharaCtl))
                    {
                        continue;
                    }

                    Vector3 hitPoint;
                    if (CheckHitTarget(_charaCtl, roleT.CharaCtl, out hitPoint))
                    {
                        _charaCtl.OnHitOther(roleT.CharaCtl,hitPoint);
                        _extends.Add(roleT.CharaCtl);
                    }
                }
            }else if (_charaCtl.RoleUnit._data.camp == ECamp.Monster)
            {
                    
                //检测玩家
                var roleT = GameManager.Inst.targetRole;
                Vector3 hitPoint;
                if (roleT.CharaCtl.hurtedEnable && !_extends.Contains(roleT.CharaCtl) && CheckHitTarget(_charaCtl, roleT.CharaCtl, out hitPoint))
                {
                    _charaCtl.OnHitOther(roleT.CharaCtl,hitPoint);
                    _extends.Add(roleT.CharaCtl);
                }
            }
        }
        
        /// <summary>
        /// 当激活伤害盒
        /// </summary>
        private void OnOpenDmgBox()
        {
            _extends.Clear();
        }

        /// <summary>
        /// 检测dmger的伤害盒是否与目标的身位盒重叠
        /// </summary>
        /// <param name="dmger"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        bool CheckHitTarget(CharacterCtl dmger, CharacterCtl other, out Vector3 hitPoint)
        {
            Rect[] rectsDmg = dmger.GetWorldDmgRects();
            if (rectsDmg != null && rectsDmg.Length > 0)
            {
                Rect[] rectsBody = other.GetWorldBodyRects();
                if (rectsBody != null)
                {
                    for (int i = 0; i < rectsDmg.Length; i++)
                    {
                        var dmgRect = rectsDmg[i];
                        if (dmgRect.size != Vector2.zero)
                        {
                            for (int j = 0; j < rectsBody.Length; j++)
                            {
                                var bodyRect = rectsBody[j];
                                if (bodyRect.size != Vector2.zero && dmgRect.Overlaps(bodyRect))
                                {
                                    hitPoint = dmgRect.center;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            hitPoint = Vector3.zero;
            return false;
        }
    }
}