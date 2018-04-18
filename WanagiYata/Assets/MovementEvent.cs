using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Assets
{
    [System.Serializable]
    public class EventSeries : UnityEvent<List<EventModel>,string>
    {
    }
}
