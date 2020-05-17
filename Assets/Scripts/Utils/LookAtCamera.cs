using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform transformToLook;
    
    private UpdateProvider updateProvider => ServiceLocator.Get<UpdateProvider>();
    
    private void Start()
    {
        updateProvider.UpdateEvent += OnUpdate;
    }

    private void OnDestroy()
    {
        updateProvider.UpdateEvent -= OnUpdate;
    }

    private void OnUpdate()
    {
        transformToLook.LookAt(Camera.main.transform.GetChild(0));    
    }
}
