using Assets.Scripts.Extension;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachine.Player
{
    class PlayerEventState : IState
    {
        private PlayerBehaviour _player;
        private Vector2 currentObjective;
        public PlayerEventState(PlayerBehaviour player, Vector2 destiny)
        {
            Debug.Log("Event");
            _player = player;
            currentObjective = destiny;
        }
        public PlayerEventState(PlayerBehaviour player)
        {
            Debug.Log("Event");
            _player = player;
            currentObjective = Vector2.zero;
        }
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 DirectorVector { get; set; }
        public object Colliding { get; private set; }

        public void Update()
        {
            if (currentObjective != Vector2.zero)
            {
                Vector2 movVector = currentObjective - (Vector2)_player.gameObject.transform.position;
                if (movVector.Equals(Vector2.zero))
                {
                    _player.stateMch.Directorvector = Vector2.zero;
                    _player.CurrentState = new PlayerIdleState(_player);
                    return;
                }
                _player.stateMch.Directorvector = movVector.normalized;
                _player.gameObject.transform.position = _player.gameObject.transform.position + (Vector3)movVector.normalized  * Time.deltaTime * _player.speed;
            }
        }
    }
}
