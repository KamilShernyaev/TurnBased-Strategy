using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
     public static ScreenShake Instance {get; private set;}
    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake() 
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        if(Instance != null)
        {
            Debug.LogError("There's more than one ScreenShake!" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Shake(float intensity = 1f) {
        cinemachineImpulseSource.GenerateImpulse(intensity);
    }
}
