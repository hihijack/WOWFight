using System;
using System.Runtime.Remoting.Messaging;
using System.Text;
using DefaultNamespace.AICore;
using DefaultNamespace.Entitys;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class DebugGUI : EditorWindow
    {
        private GUIStyle styleActive;
        private GUIStyle styleInactive;
        private GUIStyle styleCompleted;
        private GUIStyle styleFail;
        
        [MenuItem("Tools/AI信息")]
        static void AddWindow()
        {
            GetWindow<DebugGUI>(false, "AI信息");
        }
        
        private void OnGUI()
        {
            if (DebugMgr.Inst == null || DebugMgr.Inst.targetAI == null)
            {
                return;
            }

            InitStyles();

            RoleUnit_NPC target = DebugMgr.Inst.targetAI;

            GUILayout.BeginVertical();
            GUILayout.Label(target.name);
            DrawAGoal(target.GetBrain());
            GUILayout.EndVertical();
            
        }
       
        void OnInspectorUpdate()
        {
            //开启窗口的重绘，不然窗口信息不会刷新
            Repaint();
        }

        void DrawAGoal(AIGoal goal)
        {
            GUILayout.Label(goal.GetType() + ":" + goal.status, GetStyle(goal.status));
            if (goal is AIGoal_Composite)
            {
                AIGoal_Composite composGoal = goal as AIGoal_Composite;
                foreach (var subGoal in composGoal.GetSubGoals())
                {
                    DrawAGoal(subGoal);
                }
            }
        }
        
        GUIStyle GetStyle(EAIGoalStatus status)
        {
            GUIStyle style = null;
            switch (status)
            {
                case EAIGoalStatus.Inactive:
                    style = styleInactive;
                    break;
                case EAIGoalStatus.Actived:
                    style = styleActive;
                    break;
                case EAIGoalStatus.Completed:
                    style = styleCompleted;
                    break;
                case EAIGoalStatus.Fail:
                    style = styleFail;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status", status, null);
            }

            return style;
        }
        
        private void InitStyles()
        {
            //styleActive = new GUIStyle();
            styleActive.normal.textColor = Color.white;
            //styleCompleted = new GUIStyle();
            styleCompleted.normal.textColor = Color.green;
            //styleFail = new GUIStyle();
            styleFail.normal.textColor = Color.red;
            //styleInactive = new GUIStyle();
            styleInactive.normal.textColor = Color.gray;
        }
    }
}