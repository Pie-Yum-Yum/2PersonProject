using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float cameraSpeedX = 0.05f;
    [SerializeField] float cameraSpeedY = 0.05f;
    Transform player;

    void Awake()
    {
        player = GameObject.Find("Pogo Boyo").transform;
    }

    void Update()
    {
        float newX = Mathf.Lerp(transform.position.x, player.position.x, cameraSpeedX * Time.deltaTime);
        float newY = Mathf.Lerp(transform.position.y, player.position.y, cameraSpeedY * Time.deltaTime);

        transform.position = new Vector3(newX, newY, -10f);
    }
}
