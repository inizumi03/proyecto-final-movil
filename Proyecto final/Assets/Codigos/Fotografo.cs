using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fotografo : MonoBehaviour
{
    [Header("Configuraci�n de c�mara")]
    public float distanciaMaxima = 100f; // Alcance del raycast

    [Header("Puntaje")]
    public int puntosPorFoto = 10;
    private int puntajeTotal = 0;

    [Header("Referencias UI")]
    public Text textoPuntaje; // UI Text cl�sico

    [Header("Tag de los objetos fotografiables")]
    public string tagHeroe = "Heroe";

    [Header("Audio")]
    public AudioSource audioFoto;  // Sonido al tomar foto

    void Start()
    {
        ActualizarTextoPuntaje();
    }

    public void IntentarTomarFoto()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit impacto;

        if (Physics.Raycast(rayo, out impacto, distanciaMaxima))
        {
            if (impacto.collider.CompareTag(tagHeroe))
            {
                HeroeFotografiable heroe = impacto.collider.GetComponent<HeroeFotografiable>();
                if (heroe != null && !heroe.fueFotografiado)
                {
                    heroe.fueFotografiado = true;
                    puntajeTotal += puntosPorFoto;
                    ActualizarTextoPuntaje();

                    if (audioFoto != null)
                        audioFoto.Play();

                    Debug.Log("�Foto tomada al h�roe: " + impacto.collider.name + "!");
                }
                else
                {
                    Debug.Log("Este h�roe ya fue fotografiado.");
                }
            }
            else
            {
                Debug.Log("No est�s mirando a un h�roe.");
            }
        }
        else
        {
            Debug.Log("No hay nada al frente.");
        }
    }

    void ActualizarTextoPuntaje()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntajeTotal;
        }
    }
}

