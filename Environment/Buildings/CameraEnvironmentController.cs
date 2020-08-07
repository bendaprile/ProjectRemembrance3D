using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraEnvironmentController : MonoBehaviour
{
    public Transform cameraTransform;
    public CinemachineVirtualCamera virtualCamera;
    public Light sun;
    [SerializeField] Light moon;
    public float increaseCameraX = 15f;
    public float zoomFactor = 20f;
    public float outdoorBrightness = 1500f;
    public float delay = 4f;

    private float inital_x;
    private float x_axis;
    private float decreaseSunIntensity;
    private float decreaseMoonIntensity;

    private void Start()
    {
        inital_x = cameraTransform.transform.rotation.eulerAngles.x;
        x_axis = inital_x;

        decreaseSunIntensity = sun.intensity - outdoorBrightness;
        decreaseMoonIntensity = moon.intensity - outdoorBrightness;
    }

    public IEnumerator EnterBuilding()
    {

        for (float i = 0; i < delay; i += Time.deltaTime)
        {

            sun.intensity -= decreaseSunIntensity * Time.deltaTime / delay;
            moon.intensity -= decreaseMoonIntensity * Time.deltaTime / delay;

            //sun.intensity = Mathf.Lerp(sun.intensity, outdoorBrightness, Time.deltaTime);

            x_axis += increaseCameraX * Time.deltaTime / (delay);
            virtualCamera.m_Lens.FieldOfView -= zoomFactor * Time.deltaTime / (delay);
            cameraTransform.rotation = Quaternion.Euler(new Vector3(x_axis, 45, 0));

            yield return new WaitForEndOfFrame();
        }

        x_axis = Mathf.Round(x_axis);
        cameraTransform.rotation = Quaternion.Euler(new Vector3(x_axis, 45, 0));
        virtualCamera.m_Lens.FieldOfView = Mathf.Round(virtualCamera.m_Lens.FieldOfView);
    }

    public IEnumerator ExitBuilding()
    {

        for (float i = 0; i < delay; i += Time.deltaTime)
        {
            sun.intensity += decreaseSunIntensity * Time.deltaTime / delay;
            moon.intensity += decreaseMoonIntensity * Time.deltaTime / delay;

            x_axis -= increaseCameraX * Time.deltaTime / (delay);
            virtualCamera.m_Lens.FieldOfView += zoomFactor * Time.deltaTime / (delay);
            cameraTransform.rotation = Quaternion.Euler(new Vector3(x_axis, 45, 0));

            yield return new WaitForEndOfFrame();
        }

        x_axis = Mathf.Round(x_axis);
        cameraTransform.rotation = Quaternion.Euler(new Vector3(x_axis, 45, 0));
        virtualCamera.m_Lens.FieldOfView = Mathf.Round(virtualCamera.m_Lens.FieldOfView);
    }
}
