using System.Collections.Generic;
using App.Entities.Enemy;
using App.Missions.MissionEnemiesCounter;
using App.Missions.MissionGeneration.FSM;
using App.Missions.MissionGeneration.FSM.SpecificStates;
using Fusion;
using Fusion.Addons.FSM;
using UnityEngine;

namespace App.Missions.MissionGeneration
{
    public class NetMissionGenerator : NetworkBehaviour, IStateMachineOwner
    {
        [SerializeField] private Transform levelsParent;
        [SerializeField] private LevelsConfig levelsConfig;
        [SerializeField] private Mission mission;
        [SerializeField] private EnemyFactory enemyFactory;
        
        public bool MissionGenerated => _fsm.ActiveState == _generationIsOver;
        
        private StateMachine<MissionGenerationState> _fsm;

        private Idle _idle;
        private Generation _generation;
        private GenerationIsOver _generationIsOver;
        
        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            _idle = new Idle(this);
            _generation = new Generation(this, levelsParent, levelsConfig, mission, enemyFactory);
            _generationIsOver = new GenerationIsOver(this);

            _fsm = new StateMachine<MissionGenerationState>("Mission Generation", _idle, _generation,
                _generationIsOver);
            
            stateMachines.Add(_fsm);
        }
    }
}