//from https://gist.github.com/mashlol/92e66afb616ee2f1c66158cf0c2c8280

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace App.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ButtonAttribute : Attribute {}

#if UNITY_EDITOR
    [CustomEditor(typeof(UnityEngine.Object), true)]
    [CanEditMultipleObjects]
    public class BetterObjectEditor : Editor {
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
#endif
}

