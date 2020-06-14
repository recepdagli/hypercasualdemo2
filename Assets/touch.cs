using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
     private Vector3 fp;   
    private Vector3 lp;   
    private float dragDistance;  

    void Start()
    {
        dragDistance = Screen.height * 15 / 100;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0); 
            if (touch.phase == TouchPhase.Began) 
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) 
            {
                lp = touch.position;  
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {  
                        if ((lp.x > fp.x))  
                        {  
                            Debug.Log("Right Swipe");
                            main_cube.start_d_Coroutine = true;
                        }
                        else
                        {   
                            Debug.Log("Left Swipe");
                            main_cube.start_a_Coroutine = true;
                        }
                    }
                    else
                    {   
                        if (lp.y > fp.y)  
                        {   
                            Debug.Log("Up Swipe");
                            main_cube.start_w_Coroutine = true;
                        }
                        else
                        {   
                            Debug.Log("Down Swipe");
                            main_cube.start_s_Coroutine = true;
                        }
                    }
                }
                else
                {  
                    Debug.Log("Tap");
                    main_cube.touched = true;
                }
            }
        }
    }
}
