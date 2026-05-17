using DG.Tweening;
using Oculus.Interaction;
using UnityEngine;

public class BookBoxDetector : MonoBehaviour
{
    private int count;

    [SerializeField] private Transform appleToActivateDestiny;

    private void OnTriggerEnter(Collider other)
    {

        if (transform.parent.localScale != Vector3.one) return;

        other.GetComponent<DistanceGrabInteractable>().enabled = false;
        other.GetComponent<Grabbable>().enabled = false;
        other.GetComponent<GrabInteractable>().enabled = false;
        other.GetComponent<BoxCollider>().enabled = false;
        other.GetComponent<Rigidbody>().isKinematic = true;

        Sequence seq = DOTween.Sequence();
        seq.Append(other.transform.DOJump(appleToActivateDestiny.position, .1f, 1, 1f).SetEase(Ease.InOutSine));
        seq.Append(other.transform.DOMoveY(appleToActivateDestiny.transform.position.y - .1f, 1f).SetEase(Ease.InOutSine));
        seq.Join(other.transform.DOScale(.5f, 1f));
        seq.OnComplete(() =>
        {
            Destroy(other.gameObject);
            if (count == 4)
                FindAnyObjectByType<TutorialHandler>().Next();

        }
            );

        count++;



    }
}
