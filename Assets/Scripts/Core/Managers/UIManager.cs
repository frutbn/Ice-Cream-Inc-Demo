using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIManager : Manager<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject Panel_TapToPlay;
    [SerializeField] private GameObject Panel_Game;
    [SerializeField] private GameObject Panel_Finish;

    [Header("UI")]
    [SerializeField] private TMP_Text Text_MatchPercent;
    [SerializeField] private GameObject Button_NextLevelOBJ;
    [SerializeField] private GameObject Button_RetryOBJ;

    public void Button_ChangeGameObjectEnabling(GameObject obj) => obj.SetActive(!obj.activeSelf);

    public void FinishGame()
    {
        Panel_Game.SetActive(false);
        Panel_Finish.SetActive(true);

        int matchRate = CreamGenerator.Instance.IceCreamsMatchRate();
        if (matchRate > 50) Button_NextLevelOBJ.SetActive(true); else Button_RetryOBJ.SetActive(true);

        Text_MatchPercent.text = "MATCH " + matchRate + "%";
    }

    public void Button_Retry()
    {
        GameManager.Instance.ResetGame();
    }

    public void Button_NextLevel()
    {
        GameManager.Instance.ResetGame();
        LevelManager.Instance.NextLevel();
    }
}