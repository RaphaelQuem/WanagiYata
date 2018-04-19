using Assets.Scripts.Managers.Interactions;
using Assets.Scripts.Managers.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Extension
{
    public static class NPCInteractionExtension
    {
        public static string[] GetText(this NPCInteractionManager manager, PlayerBehaviour player)
        {
            try
            {
                int i = 0;
                Debug.Log(++i);
                DailyInteraction daily = manager.DailyInteractions.FirstOrDefault(d => d.DayNumber.Equals(StaticResources.CurrentDay));
                string[] result = new string[] {"",""};
                if (daily != null)
                {
                    Debug.Log(++i);
                    Interaction interaction = daily.Interactions.FirstOrDefault(it => it.Id.Equals(manager.CurrentInteractionId));
                    Debug.Log(++i);
                    if (interaction != null)
                    {
                        if (interaction.CompletionConditions != null)
                        {
                            if (player.Scalps >= interaction.CompletionConditions.Scalps && player.Skins >= interaction.CompletionConditions.Skins && (interaction.CompletionConditions.Dialogues.All(d => player.Dialogues.Contains(d)) || interaction.CompletionConditions.Dialogues.Count.Equals(0)))
                            {
                                if (interaction.QuestId != null)
                                {
                                    StaticResources.CurrentQuest = null;
                                }

                                if (interaction.SwitchTo != null)
                                {
                                    manager.CurrentInteractionId = interaction.SwitchTo;
                                    manager.InteractionNumber = 0;
                                    return new string[] { "", string.Empty };
                                }
                            }
                        }
                        
                        if(interaction.QuestId != null)
                        {
                            StaticResources.CurrentQuest = Quest.ById(interaction.QuestId);
                        }
                        if (manager.InteractionNumber <= interaction.Texts.Count - 1)
                        {
                            Debug.Log(++i);
                            result[0] = interaction.Texts[manager.InteractionNumber].Substring(0,1).Equals("§")?StaticResources.PlayerName: manager.NPCName;
                            result[1] = interaction.Texts[manager.InteractionNumber].Replace("§","");
                            manager.InteractionNumber++;
                        }
                        else
                        {
                            Debug.Log(++i);
                            manager.InteractionNumber = 0;
                            
                            return new string[] { "", string.Empty };
                        }


                        player.Dialogues.Add(interaction.Id);
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Debug.Log(JsonConvert.SerializeObject(ex));
                return new string[] { "", string.Empty };
            }
        }
    }
}
