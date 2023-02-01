using System.Collections;
using UnityEngine;

/// <summary>
/// base class cells that trigger an event
/// Contains a list of events load at runtime and a card sprite to display 
/// when an event is triggered
/// </summary>
public abstract class EventCell : Cell
{
    #region F/P
    protected Event[] events = null;

    protected Sprite cardSprite = null;

    protected WaitForSeconds waitHideCard = null;

    [SerializeField, Range(0.1f, 10f)]
    float printCardTime = 2f;

    protected abstract string SpriteCardDataPath { get; }

    protected abstract string EventDataPath { get; }
    #endregion
    void Start() => InitEventCell();

    #region CustomMethods
    protected virtual void InitEventCell()
    {
        waitHideCard = new WaitForSeconds(printCardTime);
        LoadEvents();
    }

    /// <summary>
    /// Load all EventData at a specific path.
    /// Then create events from theses datas and stock them in an array used when an event is triggered
    /// </summary>
    protected void LoadEvents()
    {
        EventData[] _datas = Resources.LoadAll<EventData>(EventDataPath);
        cardSprite = Resources.Load<Sprite>(SpriteCardDataPath);
        events = new Event[_datas.Length];
        for(int i = 0; i < _datas.Length; ++i)
        {
            if (_datas[i] == null)
                continue;

            Event _event = _datas[i].CreateEventFromData();
            _event.OnEventEnded += () =>
            {
                EndCellAction();
            };
            events[i] = _event;
        }
    }

    public override void PlayCellEffect(MonopolyCharacter _instigator)
    {
        if (events == null)
            return;

        PlayRandomEvent(_instigator);
    }

    protected void PlayRandomEvent(MonopolyCharacter _instigator)
    {
        Event _event = GetRandomEvent();
        MonopolyUIManager.Instance?.EventPanel?.PrintEventCard(cardSprite, _event.Data.EventText,
            this is CommunityEventCell);
        _event.PlayEvent(_instigator);
    }

    protected Event GetRandomEvent() => events[Random.Range(0, events.Length)];

    protected override void EndCellAction()
    {
        base.EndCellAction();
        StartCoroutine(WaitHideEventCard());
    }

    IEnumerator WaitHideEventCard()
    {
        yield return waitHideCard;

        MonopolyUIManager.Instance?.EventPanel?.HideEventCard();
    }
    #endregion
}
