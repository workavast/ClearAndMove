using App.Ai.Config;
using App.Entities;
using Fusion;
using Fusion.Addons.FSM;

namespace App.Ai.FSMs.Ai
{
    public abstract class AiState : State<AiState>
    {
        protected readonly NetAi NetAi;
        protected readonly NetEntity NetEntity;
        protected readonly AiModel AiModel;
        protected readonly AiViewZone AiViewZone;

        protected IEntity Target
        {
            get => AiModel.Target;
            set => AiModel.Target = value;
        }

        protected AiConfig AiConfig => AiModel.Config;
        protected NetworkRunner Runner => NetAi.Runner;
        
        protected AiState(NetAi netAi, NetEntity netEntity, AiModel aiModel, AiViewZone aiViewZone)
        {
            NetAi = netAi;
            NetEntity = netEntity;
            AiModel = aiModel;
            AiViewZone = aiViewZone;
        }
    }
}