using System;
using System.Collections;
using UnityEngine;

public class NextGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GoToNextGame());
    }

    // Update is called once per frame
    private IEnumerator GoToNextGame()
    {
        yield return new WaitForSeconds(10f);
        if (GeneralGameManager.Instance != null)
        {
            GeneralGameManager.Instance.NextGame();
        }
    }

}
