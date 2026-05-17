using DG.Tweening;
using UnityEngine;

public class AppearEffect : MonoBehaviour
{
    public void Appear()
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(1, 0, 1);
        transform.DOScaleY(1.0f, 1.0f);

    }
}