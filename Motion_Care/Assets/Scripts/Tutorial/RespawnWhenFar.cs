using UnityEngine;

public class RespawnWhenFar : MonoBehaviour
{
    [SerializeField] private Transform appleSpawn;

    private void Update()
    {
        if(transform.position.y < -20)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = appleSpawn.position;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
