using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 s_position = Input.mousePosition;
        s_position.z = 10.0f;
        Vector3 w_position =
        Camera.main.ScreenToWorldPoint(s_position);

        Debug.Log("x="+s_position.x);
        Debug.Log("y="+s_position.y);
        Debug.Log("z="+s_position.z);
    }
}
