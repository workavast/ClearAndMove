using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace App.Missions.MissionGeneration
{
    public class NetGenerationModel : NetworkBehaviour
    {
        [HideInInspector] public List<NetLevel> missionScheme = new();
    }
}