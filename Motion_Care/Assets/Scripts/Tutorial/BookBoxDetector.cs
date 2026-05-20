using DG.Tweening;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBoxDetector : MonoBehaviour
{
    private int count;

    [SerializeField] private Transform appleToActivateDestiny;
    [SerializeField] private List<Rigidbody> books;

    private void Awake()
    {
        foreach (var book in books) book.isKinematic = true;
        StartCoroutine(EnableRBs());
    }

    private IEnumerator EnableRBs()
    {
        yield return new WaitForSeconds(2f);
        foreach (var book in books) book.isKinematic = false;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (transform.parent.localScale != Vector3.one) return;

        other.GetComponent<DistanceGrabInteractable>().enabled = false;
        other.GetComponent<Grabbable>().enabled = false;
        other.GetComponent<GrabInteractable>().enabled = false;
        other.GetComponent<BoxCollider>().enabled = false;
        other.GetComponent<Rigidbody>().isKinematic = true;

        GetComponent<AudioSource>().Play();


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
