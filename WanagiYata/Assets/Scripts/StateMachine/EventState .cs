using Assets.Scripts.Extension;
using Assets.Scripts.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.StateMachine.Player
{
    public class PlayerEventState : IState
    {
        private PlayerBehaviour _player;
        private PlayerEvent _event;
        public PlayerEventState(PlayerBehaviour player)
        {
            Debug.Log("Talking");
            _player = player;
            _event = StaticResources.CurrentEvent;
        }
        public string Name { get; set; }
        public Vector2 DirectorVector { get; set; }
        public void Update()
        {
            EventAction action = _event.Actions.FirstOrDefault();
            if (action != null)
            {
                GameObject target = GameObject.FindGameObjectWithTag(action.Target);

                if (target.transform.position.x != action.MovementX || target.transform.position.y != action.MovementY)
                {
                    Vector3 objective = new Vector3(action.MovementX, action.MovementY);
                    Vector3 movVector = objective - target.transform.position;
                    Debug.DrawLine(target.transform.position, objective, Color.blue);
                    movVector.Normalize();
                    target.transform.position = target.transform.position + movVector * Time.deltaTime;
                }
            }
            else
                _player.CurrentState = new PlayerIdleState(_player);
        }
    }
}
