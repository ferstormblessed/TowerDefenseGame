using UnityEngine;

[CreateAssetMenu (fileName = "GameState", menuName = "ScriptableObjects/CreateGameState")]
public class GameState : ScriptableObject
{
    public enum GameStateEnum
    {
        Playing,
        GameOver
    }

    public GameStateEnum CurrentGameState;
}
