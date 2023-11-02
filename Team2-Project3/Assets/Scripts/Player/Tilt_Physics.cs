using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt_Physics : MonoBehaviour
{

    private float rotationPower = 5.0f;
    [SerializeField] private GameObject pivot ;
    Vector3 tiltLeft = new Vector3(0, 0, 5.0f);
    Vector3 tiltRight = new Vector3(0, 0, -5.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TiltLeft()
    {
        pivot.transform.Rotate(rotationPower * tiltLeft * Time.deltaTime);
    }

    public void TiltRight()
    {
        pivot.transform.Rotate(rotationPower * tiltRight * Time.deltaTime);
    }
}
