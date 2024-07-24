using UnityEngine;

// Clase base para los coleccionables
public class Collectible : MonoBehaviour
{
    // Método virtual para recoger el coleccionable
    public virtual void Collect()
    {
        // Aquí podrías agregar lógica común para todos los coleccionables
        Debug.Log("Collectible collected!");
    }
}
