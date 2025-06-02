using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientos : MonoBehaviour
{
    public Transform[] puntosDestino; // Puntos por los que se moverá el jugador
    public float velocidad = 3f;
    public float distanciaMinima = 0.1f;

    private int indiceActual = 0;
    private bool movimientoFinalizado = false;

    void Update()
    {
        if (movimientoFinalizado || puntosDestino.Length == 0) return;

        Transform destino = puntosDestino[indiceActual];
        Vector3 posicionActual = transform.position;
        Vector3 posicionObjetivo = destino.position;

        // Verifica si hay diferencia de altura (eje Y)
        bool cambiarAltura = Mathf.Abs(posicionActual.y - posicionObjetivo.y) > 0.05f;

        // Calcula la dirección hacia el destino
        Vector3 direccion = posicionObjetivo - posicionActual;

        if (!cambiarAltura)
        {
            // Si no hay diferencia de altura, no se mueve en Y
            direccion.y = 0;
        }

        // Movimiento con velocidad constante
        Vector3 movimiento = direccion.normalized * velocidad * Time.deltaTime;

        // Evita pasarse del punto objetivo
        if (movimiento.magnitude > direccion.magnitude)
        {
            transform.position = posicionObjetivo;
        }
        else
        {
            transform.position += movimiento;
        }

        // Verifica si llegó al punto
        if (Vector3.Distance(transform.position, posicionObjetivo) <= distanciaMinima)
        {
            indiceActual++;
            if (indiceActual >= puntosDestino.Length)
            {
                movimientoFinalizado = true;
            }
        }
    }
}
