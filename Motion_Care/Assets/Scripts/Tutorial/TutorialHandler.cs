using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRPlugin;

public class TutorialHandler : MonoBehaviour
{
    private bool starting;
    private int current = -1;
    [SerializeField] private List<GameObject> steps;

    private void Start()
    {
        Next();
    }

    public void Next()
    {
        if (current == 2) return;
        if (starting) return;

        StartCoroutine(NextCoroutine());

    }

    private IEnumerator NextCoroutine()
    {
        starting = true;
        yield return new WaitForSeconds(2f);

        if (current == -1) yield return new WaitForSeconds(11);

        foreach (GameObject step in steps)
        {
            step.transform.DOScale(0f, 1f).OnComplete(() =>
            {

                step.gameObject.SetActive(false);
            });

        }

        if(current != -1) GetComponent<AudioSource>().Play(); 

        if(current<2)
        current++;

        yield return new WaitForSeconds(2f);

        steps[current].gameObject.SetActive(true);
        steps[current].transform.DOScale(1f, 1f);

        starting = false;
    }
}
