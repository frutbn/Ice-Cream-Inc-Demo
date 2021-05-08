using TMPro;
using UnityEngine;

public sealed class LevelMonitor : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;

    private void OnEnable()
    {
        Text.text = "LEVEL\n" + LevelManager.Instance.GetLevel;
    }
}