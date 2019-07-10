using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// 伤害判定开关；检测伤害判定结果目标
    /// </summary>
    public class DmgCheckSys
    {
        private CharacterCtl _charaCtl;
        private bool _enable;
        private List<CharacterCtl> _extends; //判断除外列表.受击一次后加入列表.每次开启重置
        public DmgCheckSys(CharacterCtl ctl)
        {
            _charaCtl = ctl;
            _extends = new List<CharacterCtl>(10);
        }

        
        /// <summary>
        /// 伤害判定开关
        /// </summary>
        /// <param name="enable"></param>
        public void SetDmgCheckEnable(bool enable)
        {
            if (enable && !_enable)
            {
                //当开启
                _extends.Clear();
            }
            _enable = enable;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            if (_enable)
            {
                Vector3 center = new Vector3(0,0.78f,0.884f);
                Vector2 size = new Vector2(1.14f, 0.45f);
               
                //本地坐标转世界坐标
                center = _charaCtl.transform.localToWorldMatrix.MultiplyPoint(center);
                Vector3 rectPos = new Vector3(center.x - 0.5f * size.x, center.y - 0.5f * size.y, center.z);
                var boxHurt = new Rect(rectPos, size);
               
                
                
                if (_charaCtl.RoleUnit.camp == ECamp.Allies)
                {
                   //遍历所有敌人
                    foreach (var roleT in GameManager.Inst.aiCtls)
                    {
                        if (roleT.CharaCtl == _charaCtl || !roleT.CharaCtl.hurtedEnable || _extends.Contains(roleT.CharaCtl))
                        {
                            continue;
                        }

                        if (CheckACtl(boxHurt, roleT.CharaCtl))
                        {
                            _charaCtl.OnHitOther(roleT.CharaCtl,boxHurt.center);
                            _extends.Add(roleT.CharaCtl);
                        }
                    }
                }else if (_charaCtl.RoleUnit.camp == ECamp.Monster)
                {
                    
                    //检测玩家
                    var roleT = GameManager.Inst.targetRole;
                    if (roleT.CharaCtl.hurtedEnable && !_extends.Contains(roleT.CharaCtl) && CheckACtl(boxHurt, roleT.CharaCtl))
                    {
                        _charaCtl.OnHitOther(roleT.CharaCtl,boxHurt.center);
                        _extends.Add(roleT.CharaCtl);
                    }
                }
            }
        }

        bool CheckACtl(Rect hurtBox, CharacterCtl other)
        {
            var cc = other.GetCharacterController();
            Vector2 center = other.transform.position + cc.center;
            Vector2 size = new Vector2(2 * cc.radius, cc.height);
            Vector2 rectPos = new Vector2(center.x - size.x * 0.5f, center.y - size.y * 0.5f);
            //判断重叠
            Rect rt = new Rect(rectPos, size);
           
            return rt.Overlaps(hurtBox);
        }
    }
}