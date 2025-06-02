using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarCamara : MonoBehaviour
{
    private Gyroscope giroscopio;
    private bool giroscopioDisponible;

    [Header("Bloquear ejes de rotación")]
    public bool bloquearEjeX = false;
    public bool bloquearEjeY = false;
    public bool bloquearEjeZ = false;

    void Start()
    {
        giroscopioDisponible = SystemInfo.supportsGyroscope;

        if (giroscopioDisponible)
        {
            giroscopio = Input.gyro;
            giroscopio.enabled = true;
        }
        else
        {
            Debug.LogWarning("El dispositivo no tiene giroscopio.");
        }
    }

    void Update()
    {
        if (!giroscopioDisponible) return;

        // Obtener rotación del giroscopio
        Quaternion rotacionGiroscopio = giroscopio.attitude;
        rotacionGiroscopio = new Quaternion(
            rotacionGiroscopio.x,
            rotacionGiroscopio.y,
            -rotacionGiroscopio.z,
            -rotacionGiroscopio.w
        );

        Vector3 rotacionEuler = rotacionGiroscopio.eulerAngles;

        // Aplicar bloqueos si están activados
        if (bloquearEjeX) rotacionEuler.x = transform.rotation.eulerAngles.x;
        if (bloquearEjeY) rotacionEuler.y = transform.rotation.eulerAngles.y;
        if (bloquearEjeZ) rotacionEuler.z = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(rotacionEuler);
    }
}
