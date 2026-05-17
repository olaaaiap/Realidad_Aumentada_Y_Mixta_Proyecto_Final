using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private SimonDiceType type;
    [SerializeField] private SimonDiceManager manager;

    public void onCogido()
    {
        manager.RegistrarObjectoCogido(type);
    }
}
