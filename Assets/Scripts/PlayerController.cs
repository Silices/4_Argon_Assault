using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] private float controlSpeed = 10f;
    [Tooltip("In ms")][SerializeField] private float xRange = 8f;
    [Tooltip("In ms")][SerializeField] private float yRange = 4.5f;
    
    [Header("Screen-position Based")]
    [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float positionYawFactor = 6.5f;
    
    [Header("Control-throw Based")]
    [SerializeField] private float controlPitchFactor = -30f;
    [SerializeField] private float controlRollFactor = -30f;
    
    float xThrow, yThrow;


    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessTranslation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor ;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        
        float yaw = transform.localPosition.x * positionYawFactor;
        
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    
    
    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

}
