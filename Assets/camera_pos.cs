using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_pos : MonoBehaviour
{
    private GameObject main_cube_obj;
    // Start is called before the first frame update
    void Start()
    {
        main_cube_obj = GameObject.Find("main_cube");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(main_cube_obj.transform.position.x,transform.position.y,main_cube_obj.transform.position.z-15f);
    }
}
