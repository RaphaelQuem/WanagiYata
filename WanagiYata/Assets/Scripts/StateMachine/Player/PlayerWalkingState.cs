using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.StateMachine.Player
{
    public class PlayerWalkingState : IState
    {
        private PlayerBehaviour _player;
        public PlayerWalkingState(PlayerBehaviour player)
        {
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
            if (InputManager.BPressed())
            {
                _player.speed = 2.5f;
            }
            else
            {
                _player.speed = 1f;
            }
            _player.CurrentState = new PlayerWalkingState(_player);
        }
    }
}
