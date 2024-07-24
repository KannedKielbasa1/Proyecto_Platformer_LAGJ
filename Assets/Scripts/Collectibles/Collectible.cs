using UnityEngine;

// Clase base para los coleccionables
public class Collectible : MonoBehaviour
{
    // M�todo virtual para recoger el coleccionable
    public virtual void Collect()
    {
        // Aqu� podr�as agregar l�gica com�n para todos los coleccionables
        Debug.Log("Collectible collected!");
    }
}
