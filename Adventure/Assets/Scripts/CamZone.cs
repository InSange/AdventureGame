using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
    #region Fields

    #endregion

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;

    #region MonoBehavior

    private void Start()
    {
        virtualCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ãæµ¹");
        if ( other.CompareTag("Player") )
        {
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            virtualCamera.enabled = false;
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    #endregion
}
