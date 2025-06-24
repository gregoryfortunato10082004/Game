using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public GameObject panelToHide;
    public GameObject[] enemiesToActivate;
    
    void Start()
    {
        Time.timeScale = 0f; // Pausa tudo no início
        
        // Desativa todos os inimigos no início
        foreach (var enemy in enemiesToActivate)
        {
            enemy.SetActive(false);
        }
    }

    public void SetEasy()
    {
        GameDifficultyManager.Instance.currentDifficulty = Difficulty.Easy;
        Debug.Log("Dificuldade setada para Easy");
        StartGame();
    }

    public void SetHard()
    {
        GameDifficultyManager.Instance.currentDifficulty = Difficulty.Hard;
        Debug.Log("Dificuldade setada para Hard");
        StartGame();
    }

    void StartGame()
    {
        // Ativa os inimigos DEPOIS de setar a dificuldade
        foreach (var enemy in enemiesToActivate)
        {
            enemy.SetActive(true);
        }
        
        // Esconde o painel
        if (panelToHide != null)
            panelToHide.SetActive(false);
        
        // Continua o jogo
        Time.timeScale = 1f;
    }
}