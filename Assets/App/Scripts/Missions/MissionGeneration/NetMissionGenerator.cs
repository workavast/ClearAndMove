using System.Collections.Generic;
using App.Entities.Enemy;
using App.Missions.Levels;
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
        
        private readonly GenerationModel _model = new();

        private StateMachine<MissionGenerationState> _fsm;
        private Generation _generation;
        private PrepareGeneration _prepareGeneration;
        private GenerationIsOver _generationIsOver;
        
        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            _prepareGeneration = new PrepareGeneration(this, levelsConfig, _model);
            _generation = new Generation(this, levelsParent, mission, enemyFactory, _model);
            _generationIsOver = new GenerationIsOver(this);

            _fsm = new StateMachine<MissionGenerationState>("Mission Generation", _prepareGeneration,
                _generation, _generationIsOver);
            
            stateMachines.Add(_fsm);
        }
    }
}