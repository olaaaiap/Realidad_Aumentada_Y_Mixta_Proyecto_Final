using UnityEngine;

public class CardInteractable : MonoBehaviour
{
    [SerializeField] private ParejasCartasManager manager;
    [SerializeField] private CardType type;
    public CardType Type => type;


    public void OnTouched()
    {
        Debug.Log("La carta ha sido tocada correctamente");
        manager.RegistrarCarta(this);
    }

}
