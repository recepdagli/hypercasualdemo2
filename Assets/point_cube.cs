using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_cube : MonoBehaviour
{
    
    // Start is called before the first frame update
    private bool is_colli = false;
    private GameObject main_cube_obj;
    private float point_y = 0f;
    void Start()
    {
        main_cube_obj = GameObject.Find("main_cube");
    }

    // Update is called once per frame
    void Update()
    {
        if(is_colli && main_cube.end_wall_colli == false)
        {
            transform.position = new Vector3(main_cube_obj.transform.position.x,main_cube_obj.transform.position.y+point_y,main_cube_obj.transform.position.z);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name ==  "main_cube" && is_colli == false && main_cube.end_wall_colli == false)
        {
            is_colli = true;
            point_y = main_cube.point_y;
            main_cube.point_y += 0.5f;
            main_cube.puan += 1;
            Debug.Log(main_cube.puan.ToString());
            vibration.Vibrate(20);
        }
    }
}
