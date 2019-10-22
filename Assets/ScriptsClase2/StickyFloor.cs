﻿using UnityEngine;
using System.Collections;

public class StickyFloor : MonoBehaviour {

	private Vector3 m_EnterScale = Vector3.one;
    public Transform m_globalParent = null; //Por defecto el padre global será la raiz de la escena pero podría ser que no fuera así.
    public Transform m_transformToAttach;

    void Start()
    {
        if (m_transformToAttach == null)
            m_transformToAttach = transform;
    }

    void OnTriggerEnter(Collider other)
    {
        //TODO 1: Cuando el objeto que caiga sea attachable, atachamos el objeto. Ojo, la scala puede cambiar!!!
        Attachable at = other.GetComponent<Attachable>();
        if(at != null && at.IsAttachable)
        {
            m_EnterScale = other.transform.localScale;
            other.transform.parent = m_transformToAttach;
            at.IsAttached = true;
        }


    }

    void OnTriggerExit(Collider other)
    {
        //TODO 2: Cuando el objeto que caiga sea attachable, como estamos saliendo, desatachamos el objeto. Ojo, la scala puede cambiar!!!
        Attachable at = other.GetComponent<Attachable>();
        if (at != null && at.IsAttachable)
        {
            other.transform.parent = m_globalParent;
            other.transform.localScale = m_EnterScale;
            at.IsAttached = false;
        }
    }
}
