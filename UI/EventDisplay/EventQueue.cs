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
    private bool proccessing_data;

    void Start()
    {
        proccessing_data = false;
    }

    IEnumerator proccess()
    {   
        MainText.text = GetMainText(ActiveEvent.eventType);
        SecondaryText.text = ActiveEvent.SecondaryText;
        yield return new WaitForSeconds(GetDelay(ActiveEvent.eventType));
        proccessing_data = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(!uiControl.GamePaused && !proccessing_data && que.Count > 0)
        {
            proccessing_data = true;
            ActiveEvent = que.Dequeue();
            StartCoroutine(proccess());
        }
        Panel.SetActive(proccessing_data);
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
