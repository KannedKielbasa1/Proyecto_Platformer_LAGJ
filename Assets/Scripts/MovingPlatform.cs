using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;          // VELOCIDAD
    public float height = 3f;         // ALTURA
    private Vector3 startingPosition; // POSICION INICIAL

    void Start()
    {
        startingPosition = transform.position; // GUARDAR POS INICIAL
    }

    void Update()
    {
        // CALCULA NUEVA POS EN Y
        float newY = startingPosition.y + Mathf.Sin(Time.time * speed) * height;

        // ACTUALIZA POS DE LA PLATAFORMA
        transform.position = new Vector3(startingPosition.x, newY, startingPosition.z);
    }
}
