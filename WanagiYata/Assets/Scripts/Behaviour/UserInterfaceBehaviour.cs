using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.Behaviour
{
    public class UserInterfaceBehaviour:MonoBehaviour
    {
        public  Text ScalpText;

        public int ScalpCount { get; set; }
        public int SkinCount { get; set; }
        private void UpdateUI()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            ScalpCount = player.GetComponent<PlayerBehaviour>().Scalps;
            SkinCount = player.GetComponent<PlayerBehaviour>().Skins;
            ScalpText.text = "Scalps: " + ScalpCount.ToString() + " - " + SkinCount.ToString();
        }
        private void Update()
        {
            UpdateUI();
        }
    }
}