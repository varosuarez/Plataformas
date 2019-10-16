using UnityEngine;

public class Item : MonoBehaviour
{
	/// <summary>
	/// Representa el color de la puerta que se activar� cuando
	/// el jugador coja este item
	/// </summary>
	public GameManager.DoorColor m_DoorColorToActivate;

	/// <summary>
	/// GameManager presente en la escena. Necesario para activar la puerta que haga falta
	/// </summary>
	private GameObject m_GameManager = null;

	/// <summary>
	///  Inicializaciones necesarias para conseguir el Game Manager
	/// </summary>
	void Start()
	{
		// Buscamos el GameManager
		m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
	}

	/// <summary>
	/// En la funci�n de entrada en el trigger, se comprueba que sea
	/// el player el que toca el item.
	/// </summary>
	/// <param name="other">
	/// Objeto que colisiona contra el item (deber�a ser el player) <see cref="Collider"/>
	/// </param>
	void OnTriggerEnter(Collider other)
	{
		ActivateDoor(other.gameObject);
	}

	/// <summary>
	/// Funci�n que activa una puerta determinada
	/// </summary>
	public void ActivateDoor(GameObject other)
	{
		// Comprobaci�n del tag del objeto con el que se detecta colisi�n
		if (other.tag == "Player")
		{

            // TODO 1 - Obtener componente GameManager del GameObject m_GameManager y guardarlo
            // en una variable local
            GameManager gmComp = m_GameManager.GetComponent<GameManager>();

            // TODO 2 - Si el componente existe..
            if (gmComp)
            {
                // TODO 3 - llamar directamente a la funci�n ActivateDoor sobre el componente GameManager
                // Como par�metro, habr� que pasarle el m_DoorColorToActivate, y un true, indicando que SI quieres activar la puerta
                gmComp.ActivateDoor(m_DoorColorToActivate, true);


                // TODO 4 - Crear un nuevo GameObject (indicar que es necesario para crear un nuevo transform)
                // Dicho GameObject ser� el nuevo spawnPoint del player. Habr� que sumarle (0,4,0) a su posici�n para
                // que al hacer el relocate del player, �ste no atraviese el suelo
                GameObject spawn = new GameObject();
                Vector3 playerPos = other.transform.position;
                spawn.transform.position = new Vector3(playerPos.x, playerPos.y + 4, playerPos.z);


                // TODO 5 - Llamar directamente a la funci�n SetCurrentSpawnPoint sobre el componente GameManager
                // pasando como par�metro la matriz de transformaci�n de nuestro nuevo GameObject
                gmComp.SetCurrentSpawnPoint(spawn.transform);

                // TODO 6 - Autodestruirse
                //Desaparece el �tem de la escena
                // Pista: Destroy(...);
                Destroy(this.gameObject);

            }
            else
            {
                Debug.LogWarning("No existe componente GameManager dentro del Game Manager");
            }
        }
	}
}