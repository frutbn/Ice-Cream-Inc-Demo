using UnityEngine;
using PathCreation;
using System.Collections.Generic;

public class CreamGenerator : Singleton<CreamGenerator>
{
    [SerializeField] private GameObject CreamPiece;
    [SerializeField] private Transform CreamPieceSpawnPosition;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private float SpawnPieceSmoothness = 0.01f;
    [SerializeField] private List<CreamSpawnButtonMonitor> CreamSpawnArms;

    private Level LevelProperties;
    private PathCreator LevelPath;
    private List<CreamPiece> CreamPieces = new List<CreamPiece>();
    private Transform CreamPiecesParent;

    [SerializeField] private float SpawnPieceSpeed = .1f;
    private float SpawnPieceDelay = 0f;

    private float MaxSpawnablePiece;
    private float SpawnedPiece = 0f;

    private void Update()
    {
        SpawnPieceDelay = Mathf.Clamp(SpawnPieceDelay - Time.deltaTime, 0f, SpawnPieceSpeed);
    }

    public void SpawnPiece(Material pieceMat)
    {
        if (LevelPath == null) return;
        if (SpawnPieceDelay > 0f) return;

        if (SpawnedPiece >= MaxSpawnablePiece)
        {
            GameManager.Instance.FinishGame();
            return;
        }

        SpawnPieceDelay = SpawnPieceSpeed;
        SpawnedPiece += SpawnPieceSmoothness;

        var targetPos = LevelPath.path.GetPointAtDistance(SpawnedPiece, endOfPathInstruction);
        var targetRot = LevelPath.path.GetRotationAtDistance(SpawnedPiece, endOfPathInstruction);

        this.transform.position = new Vector3(targetPos.x, this.transform.position.y, targetPos.z);

        var cream = Instantiate(CreamPiece);
        cream.transform.position = CreamPieceSpawnPosition.position;

        if (CreamPieces.Count == 0 && CreamPiecesParent == null) 
        {
            GameObject newCreamPiecesParent = new GameObject();
            newCreamPiecesParent.name = "Ice Cream";
            CreamPiecesParent = newCreamPiecesParent.transform;
        }

        var creamPiece = cream.GetComponent<CreamPiece>();
        cream.transform.SetParent(CreamPiecesParent);
        CreamPieces.Add(creamPiece);
        creamPiece.SetCreamPiece(targetPos, targetRot, pieceMat);
    }

    public void DestroyIceCream()
    {
        Destroy(CreamPiecesParent.gameObject);
        foreach (CreamPiece cream in CreamPieces) Destroy(cream.gameObject);
        CreamPieces.Clear();
        SpawnedPiece = 0f;
    }

    public void PrepareLevel(Level level)
    {
        LevelProperties = level;
        LevelPath = LevelProperties.GetLevelPath;
        MaxSpawnablePiece = LevelPath.path.length;

        for (int i = 0; i < LevelProperties.LevelCreamMaterials.Count; i++)
        {
            CreamSpawnArms[i].ChangeCreamColor(LevelProperties.LevelCreamMaterials[i]);
        }
    }

    public int IceCreamsMatchRate()
    {
        int matchRate = 0;
        float totalCreamPieces = CreamPieces.Count;
        float trueCreamPieces = 0;

        for (int i = 0; i < totalCreamPieces; i++)
        {
            if (CreamPieces[i].meshRenderer.material.name == LevelProperties.LevelCreamPieces[i].meshRenderer.material.name)
            {
                trueCreamPieces++;
            }
        }

        matchRate = Mathf.RoundToInt(trueCreamPieces / totalCreamPieces * 100f);

        return matchRate;
    }
}