using UnityEngine;

public class CreamPiece : MonoBehaviour
{
    private Vector3 TargetPos;
    private Quaternion TargetRot;

    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private float RotateSpeed = 2.5f;

    public MeshRenderer meshRenderer;

    private void Awake()
    {
        SetCreamPiece(this.transform.position, this.transform.rotation, meshRenderer.material);
    }

    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, TargetPos, MoveSpeed * Time.deltaTime);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, TargetRot, RotateSpeed * Time.deltaTime);
    }

    public void SetCreamPiece(Vector3 newTargetPos, Quaternion newTargetRot, Material newMaterial)
    {
        TargetPos = newTargetPos;
        TargetRot = newTargetRot;

        meshRenderer.material = newMaterial;
    }
}