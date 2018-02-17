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
            var x = GameObject.FindGameObjectWithTag("MessageText");
            var z = GameObject.FindGameObjectWithTag("MessageBG");
            var y = GameObject.FindGameObjectWithTag("MessageCanvas");
            var t = x.GetComponent<Text>();
            if (x != null)
            {
                if (x.GetComponent<MessageTextBehaviour>().story.Equals(t.text) || t.text.Equals(string.Empty))
                {
                    var texto = _player.ActionTarget.GetComponent<NPCBehaviour>().DialogueManager.GetText(_player);
                    if (texto.Equals(string.Empty))
                    {
                        z.GetComponent<SpriteRenderer>().enabled = false;
                        x.GetComponent<MessageTextBehaviour>().enabled = false;
                        x.GetComponent<Text>().enabled = false;
                        _player.CurrentState = new PlayerIdleState(_player);
                    }
                    else
                    {
                        z.GetComponent<SpriteRenderer>().enabled = true;
                        x.GetComponent<MessageTextBehaviour>().ChangeText(texto);
                        x.GetComponent<MessageTextBehaviour>().enabled = true;
                        x.GetComponent<Text>().enabled = true;
                    }
                }
            }
        }
    }
}
