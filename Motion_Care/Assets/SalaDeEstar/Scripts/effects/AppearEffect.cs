using DG.Tweening;
using UnityEngine;

public class AppearEffect : MonoBehaviour
{
    public void Appear()
    {
        gameObject.SetActive(true);
       
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(1.0f, 1.0f);

    }
}