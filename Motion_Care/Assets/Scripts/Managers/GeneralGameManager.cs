using System.Collections;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialGO;
    [SerializeField] private GameObject comedorGO;
    [SerializeField] private GameObject salaGO;
    [SerializeField] private GameObject cocinaGO;

    private GameObject[] games;
    private int currentIndex = 0;


    public static GeneralGameManager Instance;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        games = new GameObject[]
        {
            tutorialGO,
            comedorGO,
            salaGO,
            cocinaGO
        };


        ActivateOnly(currentIndex);
    }

    public void NextGame()
    {
        StartCoroutine(NextGameRoutine());
    }

    private IEnumerator NextGameRoutine()
    {
        yield return new WaitForSeconds(3f);

        if (currentIndex < games.Length - 1)
        {
            currentIndex++;
            ActivateOnly(currentIndex);
        }
        else
        {
            Debug.Log("No hay más juegos.");
        }
    }

    private void ActivateOnly(int index)
    {
        for (int i = 0; i < games.Length; i++)
        {
            games[i].SetActive(i == index);
        }
    }
}