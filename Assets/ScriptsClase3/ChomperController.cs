using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperController : MonoBehaviour
{
    public bool run;
    public float speed;
    public float runSpeed;
    public float angle;
    public float rotationTime;
    public float timeToAccel;

    private ChomperAnimation chomperAnimation;
    private BoxCollider _boxCollider;
    private float runTime;

    /// <summary>
	/// Primer waypoint 
	/// </summary>
	public Transform m_point1 = null;

    /// <summary>
    /// Segundo waypoint
    /// </summary>
    public Transform m_point2 = null;

    /// <summary>
	/// Waypoint hacia el que se está moviendo la plataforma
	/// </summary>
	private Transform m_CurrentPoint = null;

    /// <summary>
    /// Distancia mínima al cuadrado (por eficiencia)
    /// </summary>
    private float m_MinDistanceSqr = 0.0f;

    /// <summary>
	/// Distancia a la que se considera que la plataforma ha llegado a su destino
	/// </summary>
	public float m_MinDistance = 2.0f;

    public float m_LookRadius = 4f;

    public float rotationSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        //El rigidbody se puede utilizar para rigidbody
        _boxCollider = GetComponent<BoxCollider>();
        chomperAnimation = GetComponent<ChomperAnimation>();

        m_MinDistanceSqr = m_MinDistance * m_MinDistance;
        gameObject.transform.position = m_point1.position;
        m_CurrentPoint = m_point2;
    }

    // Update is called once per frame
    void Update()
    {
        //CAMBIAMOS DE DIRECCIÓN CON UNA PROBABILIDAD DEL 5%
        //if (Random.Range(0f, 1f) >= 0.95f && !rotate)
        // StartCoroutine(Rotate(Random.Range(1, 10) % 2 == 0));

        //La velocidad depende de si está corriendo o no
        //#TODO: Habra que meter gravedad
        
        //#TODO: Habra que comprobar colisiones.
        if (!Physics.BoxCast(transform.position, _boxCollider.size,transform.forward,transform.rotation,0.1f))
        {
            GameObject player  = GameObject.FindWithTag("Player");
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if ((distance <= m_LookRadius))
            {
                _DoMovement(player.transform);
            }
            else
            {
                _DoMovement(m_CurrentPoint);
                _CheckArrived();
            }
                
        }

    }

    private float CalculateVelocity(float time)
    {
        if(run)
        {
            runTime += time;
            if (runTime > timeToAccel)
                runTime = timeToAccel;
            return Mathf.Lerp(speed, runSpeed, runTime/ timeToAccel);
        }
        else
        {
            runTime -= time;
            if (runTime < 0)
                runTime = 0f;
            return Mathf.Lerp(speed,runSpeed, runTime / timeToAccel);

        }
    }

    /// <summary>
	/// Realiza el movimiento de la plataforma
	/// </summary>
	void _DoMovement(Transform target)
    {
        // Rotate
        Vector3 direction = target.position - transform.position;
        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        targetRotation.Normalize();

        if (target.gameObject.tag != "Player")
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        else
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        // Move
        float velocity = CalculateVelocity(Time.deltaTime);
        chomperAnimation.Updatefordward(velocity / runSpeed);
        transform.position += direction * velocity * Time.deltaTime;

    }

    /// <summary>
    /// Comprueba si la plataforma ha llegado al waypoint actual
    /// </summary>
    void _CheckArrived()
    {
        // TODO 3 - Comprobar si la plataforma está a menos distancia de la distancia mínima
        float remDist = Vector3.SqrMagnitude(m_CurrentPoint.position - transform.position);
        if (remDist < m_MinDistanceSqr)
        {
            // TODO 4 - Cambiar el currentWaypoint, para que sea el contrario
            if (m_CurrentPoint != m_point1)
                m_CurrentPoint = m_point1;
            else
                m_CurrentPoint = m_point2;
        }
    }

    /// <summary>
    /// Red circle drawer in the editor
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_LookRadius);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Killing  Player");
            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            chomperAnimation.Updatefordward(0);
            StartCoroutine(chomperAnimation.Attack());            
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("RespawnPlayer");
        }
    }
}
