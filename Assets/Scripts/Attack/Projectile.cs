using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5.0f; // Tiempo de vida del proyectil

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag !="Player" && other.tag != "Background")
        {
            Debug.Log("Colisionando"+2131, other.gameObject);
            // Destruir el proyectil al impactar cualquier objeto
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        // Destruir el proyectil después de cierto tiempo
        Destroy(gameObject, lifetime);
    }
}
