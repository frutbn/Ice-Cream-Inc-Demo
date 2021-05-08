using UnityEngine;
using PathCreation;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [SerializeField] private PathCreator LevelPath;
    public PathCreator GetLevelPath => LevelPath;
    [SerializeField] private Transform LevelCreamPiecesParent;
    [HideInInspector] public List<CreamPiece> LevelCreamPieces = new List<CreamPiece>();
    public List<Material> LevelCreamMaterials;

    private void Start()
    {
        FindAllLevelCreamPieces();
    }

    private void FindAllLevelCreamPieces()
    {
        int childCount = LevelCreamPiecesParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            LevelCreamPieces.Add(LevelCreamPiecesParent.GetChild(i).GetComponent<CreamPiece>());
        }
    }
}