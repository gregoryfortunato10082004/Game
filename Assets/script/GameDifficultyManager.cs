using UnityEngine;

public enum Difficulty
{
    Easy,
    Hard
}

public class GameDifficultyManager : MonoBehaviour
{
    public static GameDifficultyManager Instance;

    public Difficulty currentDifficulty = Difficulty.Easy; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetAttackDamage()
    {
        return currentDifficulty switch
        {
            Difficulty.Easy => 10f,
            Difficulty.Hard => 20f,
            _ => 20f
        };
    }

    public float GetAttackCooldown()
    {
        return currentDifficulty switch
        {
            Difficulty.Easy => 3f,
            Difficulty.Hard => 1.5f,
            _ => 2f
        };
    }

    public float GetMoveSpeed()
    {
        return currentDifficulty switch
        {
            Difficulty.Easy => 2.0f,
            Difficulty.Hard => 3.5f,
            _ => 1.5f
        };
    }
}
