using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GirarCamara : MonoBehaviour
{
    [Header("Velocidad de giro")]
    public float velocidadRotacion = 50f;

    [Header("Bloquear ejes de rotaci�n")]
    public bool bloquearEjeX = false;
    public bool bloquearEjeY = false;

    [Header("Porcentaje de pantalla para detectar toques en los bordes")]
    [Range(0f, 0.5f)] public float anchoBorde = 0.2f; // 20 % a cada lado
    [Range(0f, 0.5f)] public float altoBorde = 0.2f;  // 20 % arriba y abajo

    private Quaternion rotacionInicial;

    void Start()
    {
        // Guardamos la rotaci�n inicial para poder centrar despu�s
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        foreach (Touch toque in Input.touches)
        {
            // Ignorar toque si est� sobre UI (bot�n, panel, etc)
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(toque.fingerId))
                continue;

            Vector2 posicion = toque.position;
            float ancho = Screen.width;
            float alto = Screen.height;

            bool enBordeDerecho = posicion.x > ancho * (1f - anchoBorde);
            bool enBordeIzquierdo = posicion.x < ancho * anchoBorde;
            bool enBordeSuperior = posicion.y > alto * (1f - altoBorde);
            bool enBordeInferior = posicion.y < alto * altoBorde;

            bool estaEnBordeHorizontal = enBordeDerecho || enBordeIzquierdo;
            bool estaEnBordeVertical = enBordeSuperior || enBordeInferior;

            // Girar c�mara si el dedo est� en alg�n borde
            if (toque.phase == TouchPhase.Stationary || toque.phase == TouchPhase.Moved)
            {
                Vector3 rotacion = Vector3.zero;

                if (!bloquearEjeY)
                {
                    if (enBordeDerecho) rotacion.y = 1f;
                    if (enBordeIzquierdo) rotacion.y = -1f;
                }

                if (!bloquearEjeX)
                {
                    if (enBordeSuperior) rotacion.x = -1f;
                    if (enBordeInferior) rotacion.x = 1f;
                }

                transform.Rotate(rotacion * velocidadRotacion * Time.deltaTime, Space.Self);
            }

            // Centrar c�mara si se toca el centro (fuera de bordes)
            bool enCentro = !estaEnBordeHorizontal && !estaEnBordeVertical;
            if (enCentro && toque.phase == TouchPhase.Began)
            {
                transform.rotation = rotacionInicial;
            }
        }
    }
}
