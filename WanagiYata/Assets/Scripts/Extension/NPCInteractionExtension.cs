using Assets.Scripts.Managers.Interactions;
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
        public static string GetText(this NPCInteractionManager manager, PlayerBehaviour player)
        {
            try
            {
                int i = 0;
                Debug.Log(++i);
                DailyInteraction daily = manager.DailyInteractions.FirstOrDefault(d => d.DayNumber.Equals(StaticResources.CurrentDay));
                string result = "...";
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
                                if (interaction.SwitchTo != null)
                                {
                                    manager.CurrentInteractionId = interaction.SwitchTo;
                                    manager.InteractionNumber = 0;
                                    return string.Empty;
                                }
                            }
                        }
                        Debug.Log(++i);

                        if (manager.InteractionNumber <= interaction.Texts.Count - 1)
                        {
                            Debug.Log(++i);
                            result = interaction.Texts[manager.InteractionNumber];
                            manager.InteractionNumber++;
                        }
                        else
                        {
                            Debug.Log(++i);
                            manager.InteractionNumber = 0;
                            return string.Empty;
                        }


                        player.Dialogues.Add(interaction.Id);
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Debug.Log(JsonConvert.SerializeObject(ex));
                return "erro";
            }
        }
    }
}
