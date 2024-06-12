using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 5.0f;

    private float screenHalfWidthInWorldUnits;
    private float screenHalfHeightInWorldUnits;

    void Start()
    {
        // CALCULAR MITAD DE LA PANTALLA EN UNIDADES DEL MUNDO
        Camera cam = Camera.main;
        Vector3 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
        screenHalfWidthInWorldUnits = screenBounds.x;
        screenHalfHeightInWorldUnits = screenBounds.y;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", moveInput);

        Vector3 horizontal = new Vector3(moveInput * speed * Time.deltaTime, 0.0f, 0.0f);
        transform.position += horizontal;

        // LOOP PARA PASAR AL PERSONAJE AL OTRO LADO DE LA PANTALLA
        Vector3 newPosition = transform.position;

        if (newPosition.x < -screenHalfWidthInWorldUnits)
        {
            newPosition.x = screenHalfWidthInWorldUnits;
        }
        else if (newPosition.x > screenHalfWidthInWorldUnits)
        {
            newPosition.x = -screenHalfWidthInWorldUnits;
        }

        transform.position = newPosition;
    }
}
