using Unity.Mathematics;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [Header("Ajustes")]
    [SerializeField] public float Sensibilidad = 100;
    [SerializeField] public Transform Player;
    [Header("Estados")]
    [SerializeField] public float RotacionHorizontal = 0;
    [SerializeField] public float RotacionVertical = 0;
    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        // Oculta el cursor mientras juegas
        Cursor.visible = false;
    }
    void Update()
    {
        //Nos dan los valores del mause para mover 
        float ValorX = Input.GetAxis("Mouse X") * Sensibilidad * Time.deltaTime;
        float ValorY = Input.GetAxis("Mouse Y") * Sensibilidad * Time.deltaTime;
        //Guarda el valor y queda en el valor para seguir
        RotacionHorizontal += ValorX;
        RotacionVertical -= ValorY;
        //Limita en 80 grados
        RotacionVertical = math.clamp(RotacionVertical, -80, 80);
        //Hace la rotaion vertical fluida
        transform.localRotation = Quaternion.Euler(RotacionVertical, 0, 0);
        //Hace la rotacion horizontal
        if (Player != null)
        {
            Player.Rotate(Vector3.up * ValorX);
        }
        else
        {
            //Si no tiene asignado el player avisas en consola
            Debug.LogWarning("Camara: asigna (Player) en el Inspector.");
        }
    }
}