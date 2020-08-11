using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventQueue : MonoBehaviour
{
    [SerializeField] private Text MainText;
    [SerializeField] private Text SecondaryText;
    [SerializeField] private GameObject Panel;
    [SerializeField] private UIController uiControl;

    private Queue<EventData> que = new Queue<EventData>();
    private EventData ActiveEvent;
    private float time_remaining;

    void Start()
    {
        time_remaining = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if(!uiControl.GamePaused && time_remaining <= 0 && que.Count > 0)
        {
            ActiveEvent = que.Dequeue();
            time_remaining = GetDelay(ActiveEvent.eventType);
            MainText.text = GetMainText(ActiveEvent.eventType);
            SecondaryText.text = ActiveEvent.SecondaryText;
        }
        else if (time_remaining > 0)
        {
            Panel.SetActive(true);
            time_remaining -= Time.deltaTime;
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void AddEvent(EventData event_in)
    {
        que.Enqueue(event_in);
    }

    private string GetMainText(EventTypeEnum eventType)
    {
        string[] text_array = { "Quest Updated:", "Quest Complete:", "Quest Started:", "Level Up" };
        return text_array[(int)eventType];
    }

    private float GetDelay(EventTypeEnum eventType)
    {
        float[] delay_array = { 2f, 2f, 2f, 2f };
        return delay_array[(int)eventType];
    }
}
