using Assets.Scripts.Managers.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Extension
{
    public static class NPCInteractionExtension
    {
        public static string GetText(this NPCInteractionManager manager, PlayerBehaviour player)
        {
            DailyInteraction daily = manager.DailyInteractions.FirstOrDefault(d => d.DayNumber.Equals(StaticResources.CurrentDay));
            string result = "...";
            if (daily != null)
            {
                
                Interaction interaction = daily.Interactions.FirstOrDefault(i => i.Id.Equals(manager.CurrentInteractionId));
                if(interaction!= null)
                {
                    if(interaction.CompletionConditions != null)
                    {
                        if(player.Scalps >= interaction.CompletionConditions.Scalps && player.Skins >= interaction.CompletionConditions.Skins && (interaction.CompletionConditions.Dialogues.All(d => player.Dialogues.Contains(d)) || interaction.CompletionConditions.Dialogues.Count.Equals(0)))
                        {
                            if (interaction.SwitchTo != null)
                            {
                                manager.CurrentInteractionId = interaction.SwitchTo;
                                manager.InteractionNumber = 0;
                                return manager.GetText(player);
                            }
                        }
                    }

                    result = interaction.Texts[manager.InteractionNumber];
                    if (manager.InteractionNumber < interaction.Texts.Count - 1)
                        manager.InteractionNumber++;

                    player.Dialogues.Add(interaction.Id);
                }
            }
            return result;
        }
    }
}
