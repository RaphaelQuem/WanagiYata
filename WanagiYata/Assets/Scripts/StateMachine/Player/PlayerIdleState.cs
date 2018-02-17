using Assets.Scripts.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.StateMachine.Player
{
    public class PlayerIdleState : IState
    {
        private PlayerBehaviour _player;
        public PlayerIdleState  (PlayerBehaviour player)
        {
            Debug.Log("Idle");
            _player = player;
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

        public void Update()
        {
            if (!InputManager.ControllerVector().Equals(Vector2.zero))
                _player.CurrentState = new PlayerWalkingState(_player);

            if (InputManager.AButton())
            {
                if (_player.CurrentAction.Equals(PlayerAction.Talk))
                    _player.CurrentState = new PlayerTalkingState(_player);
            }
        }
    }
}
