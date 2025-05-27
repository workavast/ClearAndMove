using System;
using System.Collections.Generic;

namespace App.ScenesLoading
{
    public static class ScenesConfig
    {
        public const int Bootstrap = 0;
        public const int ScenesLoading = 1;
        public const int MainMenu = 2;
        public const int Lobby = 3;
        public const int Train = 4;
        public const int Mission_1 = 5;
        public const int Mission_2 = 6;
        
        public static readonly Dictionary<int, string> SceneNamesByIndexes = new()
        {
            { Bootstrap, "BootstrapScene" },
            { ScenesLoading, "ScenesLoadingScene" },
            { MainMenu, "MainMenuScene" },
            { Lobby, "Lobby" },
            { Train, "Train" },
            { Mission_1, "Mission_1" },
            { Mission_2, "Mission_2" },
        };

        public static string NameByIndex(int sceneIndex)
        {
            if (!SceneNamesByIndexes.ContainsKey(sceneIndex))
                return default;

            return SceneNamesByIndexes[sceneIndex];
        }
        
        public static int GetIndex(this SceneType scene)
        {
            return scene switch
            {
                SceneType.BootstrapScene => Bootstrap,
                SceneType.ScenesLoadingScene => ScenesLoading,
                SceneType.MainMenuScene => MainMenu,
                SceneType.Lobby => Lobby,
                SceneType.Train => Train,
                SceneType.Mission_1 => Mission_1,
                SceneType.Missiion_2 => Mission_2,
                _ => throw new ArgumentOutOfRangeException(nameof(scene), scene, null)
            };
        }
    }

    public enum SceneType : int
    {
        BootstrapScene = 0,
        ScenesLoadingScene = 1,
        MainMenuScene = 2,
        Lobby = 4,
        Missiion_2 = 5,
        Train = 6,
        Mission_1 = 7
    }
}