using System;
using System.Collections.Generic;

public class GameStateManager
{
    // Singleton instance van GameStateManager
    private static readonly GameStateManager _instance = new GameStateManager();
    public static GameStateManager Instance => _instance;
    public Game? CurrentGame { get; private set; }
    public int CurrentTurn { get; private set; }
    public string CurrentPhase { get; private set; }
    public bool PreventAllDefenses { get; set; } = false; 
    public List<ArtifactCard> ArtifactsToDestroy { get; private set; } = new List<ArtifactCard>();

    // Private constructor voor het singleton patroon
    private GameStateManager()
    {
        CurrentTurn = 0;
        CurrentPhase = "Not Started";
    }

    // Start een nieuw spel en reset de spelstatus
    public void StartNewGame(Game game)
    {
        CurrentGame = game;
        CurrentTurn = 1;
        CurrentPhase = "Draw";
    }

    // Verhoogt de beurt en reset de verdedigingsstatus
    public void AdvanceTurn()
    {
        CurrentTurn++;
        CurrentPhase = "Draw";
        PreventAllDefenses = false;
    }

    // Stelt de huidige fase van het spel in en behandelt speciale acties
    public void SetPhase(string phase)
    {
        CurrentPhase = phase;
        if (phase == "Preparation")
        {
            DestroyScheduledArtifacts();
        }
    }

    // Plant een artefact voor vernietiging bij de volgende voorbereiding fase
    public void ScheduleArtifactDestruction(ArtifactCard artifact)
    {
        ArtifactsToDestroy.Add(artifact);
    }

    // Vernietigt geplande artefacten aan het begin van de voorbereiding fase
    private void DestroyScheduledArtifacts()
    {
        foreach (var artifact in ArtifactsToDestroy)
        {
            Console.WriteLine($"{artifact.Name} is destroyed at the beginning of the preparation phase.");
            // Hier kan logica worden toegevoegd om het artefact daadwerkelijk uit het spel te verwijderen
        }
        ArtifactsToDestroy.Clear(); // Maak de lijst leeg na vernietiging
    }

    // Haalt de huidige speler op basis van de beurt
    public Player GetCurrentPlayer()
    {
        if (CurrentGame == null || CurrentGame.Players.Count == 0)
        {
            throw new InvalidOperationException("Cannot get current player when no game is active.");
        }

        return CurrentTurn % 2 == 1 ? CurrentGame.Players[0] : CurrentGame.Players[1];
    }
}
