public class GameStateManager
{
    private static readonly GameStateManager _instance = new GameStateManager();
    public static GameStateManager Instance => _instance;

    public Game? CurrentGame { get; private set; }
    public int CurrentTurn { get; private set; }
    public string CurrentPhase { get; private set; }
    public bool PreventAllDefenses { get; set; } = false; // Voeg boolean toe voor het verdedigen
    public List<ArtifactCard> ArtifactsToDestroy { get; private set; } = new List<ArtifactCard>();

    private GameStateManager()
    {
        CurrentTurn = 0;
        CurrentPhase = "Not Started";
    }

    public void StartNewGame(Game game)
    {
        CurrentGame = game;
        CurrentTurn = 1;
        CurrentPhase = "Draw";
    }

    public void AdvanceTurn()
    {
        CurrentTurn++;
        CurrentPhase = "Draw";
        PreventAllDefenses = false; // Reset de verdedigingsstatus aan het begin van elke beurt.
    }

    public void SetPhase(string phase)
    {
        CurrentPhase = phase;
        if (phase == "Preparation")
        {
            DestroyScheduledArtifacts();
        }
    }

    public void ScheduleArtifactDestruction(ArtifactCard artifact)
    {
        ArtifactsToDestroy.Add(artifact);
    }

    private void DestroyScheduledArtifacts()
    {
        foreach (var artifact in ArtifactsToDestroy)
        {
            Console.WriteLine($"{artifact.Name} is destroyed at the beginning of the preparation phase.");
            // Logica om het artefact uit het spel te verwijderen
        }
        ArtifactsToDestroy.Clear();
    }

    public Player GetCurrentPlayer()
    {
        if (CurrentGame == null || CurrentGame.Players.Count == 0)
        {
            throw new InvalidOperationException("Cannot get current player when no game is active.");
        }

        return CurrentTurn % 2 == 1 ? CurrentGame.Players[0] : CurrentGame.Players[1];
    }
}
