using UnityEngine;

public class MovingPlatform2 : MonoBehaviour
{
    public float speed = 2f;          // VELOCIDAD
    public float distance = 3f;       // DISTANCIA POSICION INICIAL
    private Vector3 startingPosition; // POSICION INICIAL

    void Start()
    {
        startingPosition = transform.position; // GUARDA POS INICIAL
    }

    void Update()
    {
        // CALCULA NUEVA POS X
        float newX = startingPosition.x + Mathf.Sin(Time.time * speed) * distance;

        // ACTUALIZA POS DE LA PLATAFORMA
        transform.position = new Vector3(newX, startingPosition.y, startingPosition.z);
    }
}
