using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterKey : MonoBehaviour
{
    public string m_lastSceneName;

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(m_lastSceneName);
    }
}
