using DG.Tweening;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public void Disappear()
    {
        transform.DOScale(0.0f, 1.0f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
