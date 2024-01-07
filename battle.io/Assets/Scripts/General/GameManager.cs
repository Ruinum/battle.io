using DG.Tweening;
using Ruinum.Core.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Ruinum.Core.Systems
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;

        private List<ISystem> _systems = new List<ISystem>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
            new Game(_gameConfig);
            DOTween.SetTweensCapacity(700, 10);
        }

        private void Start()
        {
            Application.targetFrameRate = 60;    

            _systems.Add(new ExecuteSystem());
            _systems.Add(new TimerSystem());
            _systems.Add(new CoroutineSystem());
            _systems.Add(new SimulatePhysicSystem());
            _systems.Add(new SceneSystem());
            _systems.Add(new GameSystems());
            _systems.Add(new StatsSystem(_gameConfig.PlayerStats));
            _systems.Add(new AchievementSystem(_gameConfig));

            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].InitializeSystem();
            }
        }

        private void Update()
        {
            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].Execute();
            }
        }
    }
}