using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Locomotioncontroller : MonoBehaviour
{
    public XRController rightTeleport;
    public XRController leftTeleport;
    public InputHelpers.Button teleportActivation;
    public float activationThres = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rightTeleport)
        {
            rightTeleport.gameObject.SetActive(CheckIfAct(rightTeleport));
        }
        if (leftTeleport)
        {
            leftTeleport.gameObject.SetActive(CheckIfAct(leftTeleport));
        }
    }
    public bool CheckIfAct(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivation, out bool isAct, activationThres);
        return isAct;
    }
}
