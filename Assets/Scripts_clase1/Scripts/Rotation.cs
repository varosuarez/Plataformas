using UnityEngine;

public class Rotation : MonoBehaviour
{

    /// <summary>
    /// Velocidad de rotaci�n
    /// </summary>
    public Vector3 m_RotationSpeed = Vector3.zero;

    /// <summary>
    /// En funci�n de la rotaci�n que se pasa desde fuera, se rota el objeto
    /// </summary>
    void Update()
    {
        transform.Rotate(m_RotationSpeed * Time.deltaTime);
    }
}