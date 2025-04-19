using UnityEngine;
using Zenject;

namespace App.GameState
{
    public class GameStateInstaller : MonoInstaller
    {
        [SerializeField] private NetGameState gameState;

        public override void InstallBindings()
        {
            Container.Bind<NetGameState>().FromInstance(gameState).AsSingle();
        }
    }
}