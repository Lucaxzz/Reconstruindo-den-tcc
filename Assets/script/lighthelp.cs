using UnityEngine;

public class lighthelp : MonoBehaviour
{
    public float bounceHeight = 0.5f; // Altura do pulo
    public float bounceSpeed = 2f; // Velocidade da animação

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
