using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreamSpawnButtonMonitor : MonoBehaviour
{
    [SerializeField] private Material CreamMat;
    [SerializeField] private Image Image_ColorCream;
    [SerializeField] private TMP_Text Text_Cream;
    [SerializeField] private MeshRenderer CreamArm;
    [SerializeField] private Animator CreamArmAnimator;

    private bool Spawn = false;

    private void Update()
    {
        CreamArmAnimator.SetBool("Arm Enabling", Spawn);

        if (!Spawn) return;

        CreamGenerator.Instance.SpawnPiece(CreamMat);
    }

    public void Button_SpawnPiece(bool spawn) => Spawn = spawn;

    public void ChangeCreamColor(Material cream)
    {
        CreamMat = cream;

        Image_ColorCream.color = CreamMat.color;
        CreamArm.material = CreamMat;
        Text_Cream.text = CreamMat.name;
    }

    private void OnDisable()
    {
        Spawn = false;
    }
}