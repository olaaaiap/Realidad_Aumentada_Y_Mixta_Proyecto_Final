using UnityEngine;

public class Drop : MonoBehaviour
{
    public bool contado = false;

    private void Update()
    {
        //Destruir cuando baje de un valor de y de -0.5
        if(transform.position.y < -0.5)
        {
            Destroy(gameObject);
        }
    }
}