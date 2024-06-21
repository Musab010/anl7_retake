public class GameEvent
{
    public string Message { get; set; }
    public string EffectType { get; set; }

    // Constructor om een GameEvent te initialiseren met een bericht en een optioneel effecttype
    public GameEvent(string message, string effectType = "")
    {
        Message = message;
        EffectType = effectType;
    }
}

