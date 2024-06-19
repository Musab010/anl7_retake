public class GameEvent
{
    public string Message { get; set; }
    public string EffectType { get; set; } // Add this to specify the type of effect, if necessary

    public GameEvent(string message, string effectType = "")
    {
        Message = message;
        EffectType = effectType;
    }
}
