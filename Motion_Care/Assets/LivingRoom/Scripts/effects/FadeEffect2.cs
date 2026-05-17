using DG.Tweening;
using UnityEngine;

public class FadeEffect2 : MonoBehaviour
{
    public void Disappear()
    {
        transform.DOScaleZ(0.0f, 1.0f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
