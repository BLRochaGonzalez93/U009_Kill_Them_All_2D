using UnityEngine;

public class VariableEnums : MonoBehaviour
{
    // Define the different Racial Classes of the game
    public enum RacialType { Knight, Hunter, Assassin, Sorcerer, Cleric, Barbarian}

    // Define the different states of the game
    public enum GameState { Gameplay, Paused, GameOver, LevelUp }
}
