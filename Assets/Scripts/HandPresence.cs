using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    // Start is called before the first frame update
    private InputDevice targetDevice;
    public List<GameObject> controllerPrefab;
    private GameObject spawnedController;
    public InputDeviceCharacteristics ControlChars;
    public GameObject handModle;
    private GameObject spawnedHand;
    public bool showController = false;
    private Animator animator;
    public bool Grabbing { get; set; } = false;



    void Start()
    {
        TryInitialize(); 
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices);

        InputDevices.GetDevicesWithCharacteristics(ControlChars, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefab.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find controller");
                spawnedController = Instantiate(controllerPrefab[0], transform);
            }

            spawnedHand = Instantiate(handModle, transform);
            animator = spawnedHand.GetComponent<Animator>();
        }
        if(targetDevice.name != null)
        {
            UpdatePlay();
        }
    }
    void UpdatePlay()
    {
        if (Grabbing)
        {
            Grabbing = true;
        }
        else
        {
            Grabbing = false;
        }
    }
    void UpdateHandAnimaion()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigVal))
        {
            animator.SetFloat("Trig", trigVal);
        }
        else
        {
            animator.SetFloat("Trig", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gropVal))
        {
            animator.SetFloat("grip", gropVal);
        }
        else
        {
            animator.SetFloat("grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log("Pressing Prime Button");

        
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigVal) && trigVal > .1f)
        {
            Debug.Log("Pressing Trig" + trigVal);
        }

        
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 touchVal) && touchVal != Vector2.zero)
        {
            Debug.Log("Touch Pad" + touchVal);
        }*/
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHand.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHand.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimaion();
            }
        }

        
    }
}
