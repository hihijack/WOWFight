using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.GizmoHelper
{
    public class SkillBoxInfo : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            var style = new GUIStyle();
            style.normal.textColor = Color.black;
            Handles.Label(transform.position, "X:" + transform.localPosition.z.ToString("f2") + ",Y:" + transform.localPosition.y.ToString("f2")
                                              + "\nsizeX:" + transform.localScale.z.ToString("f2") + ",sizeY:" + transform.localScale.y.ToString("f2"),style);
        }
    }
}