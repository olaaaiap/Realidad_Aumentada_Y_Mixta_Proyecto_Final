using UnityEngine;

public class ReinicioCaidaManager : MonoBehaviour
{
    [Header("Altura mínima antes de reiniciar")]
    public float alturaCaidaY = 0.0f;

    private Vector3 posicionGuardada;
    private Quaternion rotacionGuardada;
    private bool posicionGuardadaLista = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (posicionGuardadaLista && transform.position.y < alturaCaidaY)
        {
            ReiniciarObjeto();
        }
    }

    public void GuardarPosicionForzada()
    {
        posicionGuardada = transform.position;
        rotacionGuardada = transform.rotation;
        posicionGuardadaLista = true;

        Debug.Log($"{name} → posición guardada desde FixAppearAnimation2: {posicionGuardada}");
    }

    private void ReiniciarObjeto()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.position = posicionGuardada;
        transform.rotation = rotacionGuardada;

        rb.isKinematic = false;

        Debug.Log($"{name} → reseteado a su posición guardada");
    }
}

