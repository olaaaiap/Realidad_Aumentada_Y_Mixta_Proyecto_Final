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
        // Si ya tenemos posición guardada y el objeto cae por debajo del límite
        if (posicionGuardadaLista && transform.position.y < alturaCaidaY)
        {
            ReiniciarObjeto();
        }
    }

    // Llamado desde FixAppearAnimation2 cuando el objeto aparece en la mesa
    public void GuardarPosicionForzada()
    {
        posicionGuardada = transform.position;
        rotacionGuardada = transform.rotation;
        posicionGuardadaLista = true;

        Debug.Log($"{name} → posición guardada desde FixAppearAnimation2: {posicionGuardada}");
    }

    private void ReiniciarObjeto()
    {
        // Resetear física
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Volver a la posición guardada
        transform.position = posicionGuardada;
        transform.rotation = rotacionGuardada;

        // Reactivar física
        rb.isKinematic = false;

        Debug.Log($"{name} → reseteado a su posición guardada");
    }
}

