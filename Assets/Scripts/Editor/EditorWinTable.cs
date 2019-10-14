using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Editor
{
    public class EditorWinTable : EditorWindow
    {
        private Object target;
        private UnityEditor.Editor targetEditor;
        
        [MenuItem("Assets/在表格窗口打开")]
        static void AddWindow()
        {
            //GetWindow<EditorWinTable>(false, "表格窗口").SetTarget(Selection.activeObject);
            EditorWinTable inst = CreateInstance<EditorWinTable>();
            inst.Show();
            inst.SetTarget(Selection.activeObject);
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            target = EditorGUILayout.ObjectField("目标:" , target, typeof(ScriptableObject));
            if (EditorGUI.EndChangeCheck())
            {
                targetEditor = UnityEditor.Editor.CreateEditor(target);
            }

            if (targetEditor != null)
            {
                targetEditor.OnInspectorGUI();
            }
        }

        public void SetTarget(Object target)
        {
            this.target = target;
            targetEditor = UnityEditor.Editor.CreateEditor(target);
            this.title = target.name;
        }
    }
}