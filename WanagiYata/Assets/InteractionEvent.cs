using UnityEngine.Events;

namespace Assets
{
    [System.Serializable]
    public class InteractionEvent : UnityEvent<string>, IEvent
    {
        public void Invoke()
        {
        }
    }
}
