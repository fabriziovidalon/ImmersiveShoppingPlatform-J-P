using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform camara;
    private CharacterController controlador;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 5f;

    [Header("Gravedad")]
    [SerializeField] private float GravedadDelJugador = -9f;
    private Vector3 velocidadVertical;

    private void Awake()
    {
        controlador = GetComponent<CharacterController>();

        if (camara == null && Camera.main != null)
            camara = Camera.main.transform;
    }

    void Update()
    {
        MoverJugadorEnPlano();
        AplicarGravedad();
    }

    private void MoverJugadorEnPlano()
    {
        //Capturamos las teclas (AWSD y Flechas)
        float ValorHorizontal =Input.GetAxisRaw("Horizontal");
        float ValorVertical =Input.GetAxisRaw("Vertical");

        //Calculamos hacia donde mira la camara solo en Eje (Adelante y atras) y (Detecha y Izquierda)
        Vector3 adelanteCamara = camara.forward;
        Vector3 derechaCamara = camara.right;

        //Eliminamos Eje Y porque no necesitamos
        adelanteCamara.y = 0f;
        derechaCamara.y = 0f;

        //Normaliza para no tener valores diferentes
        adelanteCamara.Normalize();
        derechaCamara.Normalize();

        //Combina para tener flechas diagonales 
        Vector3 direccionplano = (derechaCamara * ValorHorizontal + adelanteCamara * ValorVertical);

        //Hace que cuando precione dos direcciones en diagonal valla mas rapido
        if (direccionplano.sqrMagnitude > 0.0001f)
            direccionplano.Normalize();

        //Le da Velocidad y que dependa del tiempo no de los FPS
        Vector3 desplazamientoXZ = direccionplano * (velocidadMovimiento * Time.deltaTime);
        controlador.Move(desplazamientoXZ);

    }

    private void AplicarGravedad()
    {
        //Aplica fuerza de gravedad
        velocidadVertical.y += GravedadDelJugador * Time.deltaTime;
        //Mueve al jugador con la gravedad
        controlador.Move (velocidadVertical * Time.deltaTime);

        //Controla que Si esta callendo tenga un peso al caer controlado
        if (controlador.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;
        }
    }
}