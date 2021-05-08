using UnityEngine;

public sealed class GameManager : Manager<GameManager>
{
    private bool isGameStarted = false;
    public bool GetIsGameStarted => isGameStarted;

    private void Start()
    {
        LevelManager.Instance.OpenLevel(LevelManager.Instance.GetLevel);
    }

    public void StartGame()
    {
        isGameStarted = true;
    }

    public void FinishGame()
    {
        UIManager.Instance.FinishGame();
    }

    public void ResetGame()
    {
        isGameStarted = false;

        CreamGenerator.Instance.DestroyIceCream();
    }
}