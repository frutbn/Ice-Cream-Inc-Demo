using System.Collections.Generic;
using UnityEngine;

public sealed class LevelManager : Manager<LevelManager>
{
    private int Level = 1;
    public int GetLevel => Level;
    [SerializeField] private List<GameObject> Levels;

    private Level CurrentLevelProperties;

    public void OpenLevel(int level)
    {
        Levels[level - 1].SetActive(true);
        CurrentLevelProperties = Levels[GetLevel - 1].GetComponent<Level>();
        CreamGenerator.Instance.PrepareLevel(CurrentLevelProperties);
    }

    public void SetLevel(int value)
    {
        Level = value;
        Level = Mathf.Clamp(Level, 1, Levels.Count);
    }

    public void NextLevel()
    {
        Levels[GetLevel - 1].SetActive(false);
        SetLevel(GetLevel + 1);
        OpenLevel(GetLevel);
    }
}