using UnityEngine;

namespace App.Utils.Attributes.HideIfOverride
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