using UnityEngine;

public class Drop : MonoBehaviour
{
    public bool counted = false;

    private void Update()
    {
        if(transform.position.y < -0.5)
        {
            Destroy(gameObject);
        }
    }
}