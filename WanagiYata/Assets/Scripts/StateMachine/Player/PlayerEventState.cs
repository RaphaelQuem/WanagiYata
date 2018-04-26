using Assets.Scripts.Extension;
using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.StateMachine.Player
{
    class PlayerEventState : IState
    {
        private PlayerBehaviour _player;
        private List<EventModel> actionList;
        private string _eventName;
        public PlayerEventState(PlayerBehaviour player, List<EventModel> list, string eventName)
        {
            Debug.Log("Event");
            _player = player;
            actionList = list;
            _eventName = eventName;
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
            var current = actionList[0];
            if (current != null)
            {
                if (current.DestinationX > 0 || current.DestinationY > 0)
                    MovementEvent(current);
                else
                    InteractionEvent(current);
            }
            else
                _player.CurrentState = new PlayerIdleState(_player);
        }

        private void InteractionEvent(EventModel current)
        {


            var y = GameObject.FindGameObjectWithTag("MessageCanvas").GetComponent<UIModel>();

            if (InputManager.AButton())
            {
                var texto = EventManager.GetText(_eventName);

                y.MessaageBG.GetComponent<SpriteRenderer>().enabled = true;
                y.MessageText.GetComponent<MessageTextBehaviour>().ChangeText(texto[1]);
                y.MessageText.GetComponent<MessageTextBehaviour>().enabled = true;
                y.MessageText.GetComponent<Text>().enabled = true;
                y.CharacterName.GetComponent<Text>().text = texto[0];
                y.CharacterName.SetActive(true);

                if (texto[0].Equals(string.Empty))
                {
                    DisableDialogueBox(y);
                    actionList.Remove(actionList[0]);
                }

            }
            if (y.MessageText.GetComponent<MessageTextBehaviour>().story.Equals(y.MessageText.GetComponent<Text>().text) || y.MessageText.GetComponent<Text>().text.Equals(string.Empty))
            {


                if (!y.MessageText.GetComponent<Text>().enabled)
                {
                    var texto = EventManager.GetText(_eventName);
                    y.MessaageBG.GetComponent<SpriteRenderer>().enabled = true;
                    y.MessageText.GetComponent<MessageTextBehaviour>().ChangeText(texto[1]);
                    y.MessageText.GetComponent<MessageTextBehaviour>().enabled = true;
                    y.MessageText.GetComponent<Text>().enabled = true;
                    y.CharacterName.GetComponent<Text>().text = texto[0];
                    y.CharacterName.SetActive(true);

                    if (texto[0].Equals(string.Empty))
                    {
                        DisableDialogueBox(y);
                        actionList.Remove(actionList[0]);
                    }
                }
            }


        }

        private void DisableDialogueBox(UIModel y)
        {
            y.MessaageBG.GetComponent<SpriteRenderer>().enabled = false;
            y.MessageText.GetComponent<MessageTextBehaviour>().enabled = false;
            y.MessageText.GetComponent<Text>().enabled = false;
            y.CharacterName.SetActive(false);
            _player.CurrentState = new PlayerIdleState(_player);
        }
        private void MovementEvent(EventModel current)
        {
            if (current.DestinationX != null || current.DestinationY != null)
            {
                Vector2 currentObjective = new Vector2((int)current.DestinationX, (int)current.DestinationY);

                Vector2 movVector = currentObjective - (Vector2)_player.gameObject.transform.position;
                if (movVector.x < 0.05 && movVector.y < 0.05)
                {
                    actionList.Remove(actionList[0]);
                    _player.stateMch.Directorvector = Vector2.zero;
                    if (actionList.Count.Equals(0))
                        _player.CurrentState = new PlayerIdleState(_player);
                    return;
                }
                _player.stateMch.Directorvector = movVector.normalized;
                _player.gameObject.transform.position = _player.gameObject.transform.position + (Vector3)movVector.normalized * Time.deltaTime * _player.speed;
            }
        }
    }
}
