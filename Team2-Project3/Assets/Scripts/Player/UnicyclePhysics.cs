using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicyclePhysics : MonoBehaviour
{
    [SerializeField] private GameObject seat;
    private float rotationPower = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationPower = rotationPower + (1f * Time.deltaTime);
        transform.Rotate(rotationPower * Time.deltaTime, 0, 0, Space.Self);
        seat.transform.Rotate(-rotationPower * Time.deltaTime, 0, 0, Space.Self);
    }
}
