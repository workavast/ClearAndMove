using UnityEngine;

namespace App.Tools.Attributes.HideIfOverride
{
    public class HideIfOverrideAttribute : PropertyAttribute
    {
        public string MethodName { get; }

        public HideIfOverrideAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}