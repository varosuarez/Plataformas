using UnityEngine;

public class TriggerLoadAdditive : MonoBehaviour
{
    // TODO 1 - A�adir string p�blico que ser� el nombre del nivel a cargar
    public GameManager.DoorColor m_levelToLoad;
    /// <summary>
    /// La comprobaci�n del tipo de gameobject que entra en el trigger se hace por tag
    /// El valor del tag que nos interesa se guarda en esta variable
    /// </summary>
    private string m_PlayerTag = "Player";

	/// <summary>
	/// Detecta cu�ndo un GameObject entra en el trigger al cual est� asignado este componente.
	/// En nuestro caso, realizamos la carga aditiva del nivel indicado en el atributo
	/// p�blico "LevelToLoadName"
	/// </summary>
	void OnTriggerEnter(Collider other)
	{
		LoadLevelAdditive(other.gameObject);
	}


    // TODO 2 - Hacer funci�n que cargue un nivel de forma aditiva.
    // Ser� necesario pasarle el gameObject que ha colisionado con el trigger
    // Tras cargar el nivel, desactivamos el trigger
    private void LoadLevelAdditive(GameObject obj)
    {
        if(obj.tag.Equals(m_PlayerTag))
        {
            GameObject gObj = GameObject.FindGameObjectWithTag("GameManager");
            GameManager gManager = gObj.GetComponent<GameManager>();
            gManager.TriggerLoadAdditive(m_levelToLoad);
        }
    }


}