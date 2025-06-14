#if UNITY_EDITOR

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace App.Utils.Attributes.Button
{
    [CustomEditor(typeof(UnityEngine.Object), true)]
    [CanEditMultipleObjects]
    public class ButtonAttributeEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            var methods = target.GetType().GetMethods(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );

            foreach (MethodInfo method in methods) {
                if (method.GetCustomAttribute<ButtonAttribute>() == null)
                    continue;

                if (GUILayout.Button(method.Name))
                    method.Invoke(target, new object[0]);
            }
        }
    }
}
#endif
