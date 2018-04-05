using Assets.Scripts.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.StateMachine.Player
{
    public class PlayerTalkingState : IState
    {
        private PlayerBehaviour _player;
        public PlayerTalkingState(PlayerBehaviour player)
        {
            Debug.Log("Talking");
            _player = player;
            Talk();
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
            if (InputManager.AButton())
                Talk();
        }
        public void Talk()
        {

            var y = GameObject.FindGameObjectWithTag("MessageCanvas").GetComponent<UIModel>();

            if (y.MessageText.GetComponent<MessageTextBehaviour>().story.Equals(y.MessageText.GetComponent<Text>().text) || y.MessageText.GetComponent<Text>().text.Equals(string.Empty))
            {
                var texto = _player.ActionTarget.GetComponent<NPCBehaviour>().DialogueManager.GetText(_player);
                if (texto[0].Equals(string.Empty))
                {
                    y.MessaageBG.GetComponent<SpriteRenderer>().enabled = false;
                    y.MessageText.GetComponent<MessageTextBehaviour>().enabled = false;
                    y.MessageText.GetComponent<Text>().enabled = false;
                    y.CharacterName.SetActive(false);
                    _player.CurrentState = new PlayerIdleState(_player);
                }
                else
                {
                    y.MessaageBG.GetComponent<SpriteRenderer>().enabled = true;
                    y.MessageText.GetComponent<MessageTextBehaviour>().ChangeText(texto[1]);
                    y.MessageText.GetComponent<MessageTextBehaviour>().enabled = true;
                    y.MessageText.GetComponent<Text>().enabled = true;
                    y.CharacterName.GetComponent<Text>().text = texto[0];
                    y.CharacterName.SetActive(true);

                }
            }

        }
    }
}
