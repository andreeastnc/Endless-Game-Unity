using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    // Initializat cu null ca sa nu mai primim warning in unity
    [SerializeField] Transform player = null;
    Vector3 offset;

    // Start is called before the first frame update
    private void Start()
    {
    offset = transform.position - player.position; 
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetPos = player.position + offset;
        targetPos.x = 0;
        transform.position = targetPos;
    }
}
