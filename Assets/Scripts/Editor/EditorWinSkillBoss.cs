using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Editor
{
    public class EditorWinSkillBoss : EditorWindow
    {
        [MenuItem("GameObject/添加到技能盒")]
        static void AddWindow()
        {
            GetWindow<EditorWinSkillBoss>(false, "盒编辑器");
        }

        private int skillID;
        
        
        private void OnGUI()
        {
            if (Selection.activeGameObject == null)
            {
                return;
            }
            
        }
    }
}