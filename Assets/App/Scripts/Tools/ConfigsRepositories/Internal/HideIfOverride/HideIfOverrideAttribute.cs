using UnityEngine;

namespace App.Tools.ConfigsRepositories.HideIfOverride
{
    internal class HideIfOverrideAttribute : PropertyAttribute
    {
        internal string MethodName { get; }

        internal HideIfOverrideAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}