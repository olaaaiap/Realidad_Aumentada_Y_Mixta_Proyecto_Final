using DG.Tweening;
using UnityEngine;

public class TurnEffect : MonoBehaviour
{
    private bool faceUp = false;
    private float transformInicial;
    private Vector3 sizeMesh;
    public void Girar()
    {
        transformInicial = transform.position.y;
        sizeMesh = GetComponent<MeshRenderer>().bounds.size;
        faceUp = !faceUp;
        if (faceUp)
        {
            transform.DOMoveY(transformInicial + 0.1f, 0.5f)
                .OnComplete(() =>
                {
                    transform.DORotate(new Vector3(-90, 0, 180), 0.5f, RotateMode.Fast)
                       .OnComplete(() =>
                       {
                           transform.DOMoveY(transformInicial + sizeMesh.y, 0.5f, false);
                       });
                });
        }
        else
        {
            transform.DOMoveY(transformInicial + 0.1f, 0.5f)
                .OnComplete(() =>
                {
                    transform.DORotate(new Vector3(90, 0, 0), 0.5f, RotateMode.Fast)
                       .OnComplete(() =>
                       {
                           transform.DOMoveY(transformInicial - sizeMesh.y, 0.5f, false);
                       });
                });
        }
    }

}
