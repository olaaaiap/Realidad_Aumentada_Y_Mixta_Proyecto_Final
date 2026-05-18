using DG.Tweening;
using UnityEngine;

public class PlateDetector : MonoBehaviour
{
    [SerializeField] private GameObject appleTDetect;
    [SerializeField] private GameObject appleToActivate;
    [SerializeField] private Transform appleToActivateDestiny;

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.localScale != Vector3.one) return;

        if(other.gameObject == appleTDetect)
        {
            GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            appleToActivate.SetActive(true);

            appleToActivate.transform.position = other.transform.position;
            appleToActivate.transform.DOJump(appleToActivateDestiny.position, .1f, 1, 1f).SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {

                    FindAnyObjectByType<TutorialHandler>().Next();
                });

        }
    }


}
