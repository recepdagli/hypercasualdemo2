using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class main_cube : MonoBehaviour
{
    private Rigidbody body;
    public Texture btnTexture;
    public static int puan = 0;
    public static float point_y = 0.5f;
    public static bool end_wall_colli = false;
    public float speed;

    public static bool start_s_Coroutine = false;
    public static bool start_a_Coroutine = false;
    public static bool start_d_Coroutine = false;
    public static bool start_w_Coroutine = false;

    public static bool touched = false;
    
    bool gameover = false;
    public int scene_number = 0;

    public float olcek;
    void Start()
    {
        scene_number = int.Parse(SceneManager.GetActiveScene().name.Substring(SceneManager.GetActiveScene().name.Length - 1));
            
        body = GetComponent<Rigidbody>();
        olcek = Screen.width/7f;
    }
    IEnumerator end_Coroutine(int scene_number)//başka bölüme geçmeden (veya yeniden başlamadan) önce 3 sn bekle
    {
        Debug.Log("basladi" + Time.time);

        yield return new WaitForSeconds(3);
        sifirla();
        SceneManager.LoadScene("scene"+scene_number.ToString());
        Debug.Log("bitti" + Time.time);
    }
    IEnumerator s_Coroutine()
    {
        etiket:
        body.velocity = Vector3.back*speed;

        yield return new WaitForSeconds(0.5f);

        if(body.velocity.magnitude != 0)
        {
            goto etiket;
        }
    }
    IEnumerator a_Coroutine()
    {
        etiket:
        body.velocity = Vector3.left*speed;

        yield return new WaitForSeconds(0.5f);

        if(body.velocity.magnitude != 0)
        {
            goto etiket;
        }
    }
    IEnumerator w_Coroutine()
    {
        etiket:
        body.velocity = Vector3.forward *speed;

        yield return new WaitForSeconds(0.5f);

        if(body.velocity.magnitude != 0)
        {
            goto etiket;
        }
    }
    IEnumerator d_Coroutine()
    {
        etiket:
        body.velocity = Vector3.right*speed;

        yield return new WaitForSeconds(0.5f);

        if(body.velocity.magnitude != 0)
        {
            goto etiket;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //sadece 2.levelde çalışan boşluğa gittikten sonra yeniden başlatan kısım
        if(transform.position.x < -5f && gameover == false && scene_number == 2)
        {
            body.velocity = Vector3.zero;
            body.isKinematic = false;
            body.constraints = RigidbodyConstraints.None;
            body.mass = 1f;
            Gameover(true);
        }
        //Debug.Log(body.velocity.magnitude.ToString());
        if ((Input.GetKey("s") || start_s_Coroutine) && gameover == false && touched)//s
        {  
            start_s_Coroutine = false;
            StopAllCoroutines();
            StartCoroutine(s_Coroutine());
        }
        if ((Input.GetKey("w") || start_w_Coroutine) && gameover == false && touched)//w
        {
            start_w_Coroutine = false;
            StopAllCoroutines();
            StartCoroutine(w_Coroutine());
        }
        if ((Input.GetKey("d") || start_d_Coroutine) && gameover == false && touched)//d
        {
            start_d_Coroutine = false;
            StopAllCoroutines();
            StartCoroutine(d_Coroutine());
        }
        if ((Input.GetKey("a") || start_a_Coroutine) && gameover == false && touched)//a
        {
            start_a_Coroutine = false;
            StopAllCoroutines();
            StartCoroutine(a_Coroutine());
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name ==  "end_wall" && end_wall_colli == false && puan == 8)
        {
            Gameover(false);
        }
    }
    void Gameover(bool restart)
    {
        gameover = true;
        //levelin bitişi
        end_wall_colli = true;
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach(GameObject go in allObjects)
        {
            Debug.Log(go.name);
            if(go.name.IndexOf("oint_cube") > 0)
            {
                //go.GetComponent<Rigidbody>().useGravity = false;
                go.GetComponent<Rigidbody>().isKinematic = false;
                go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                go.GetComponent<Rigidbody>().mass = 1f;
            }
        }
        vibration.Vibrate(500);
        //bu sahne hariç başka bir sahne aç
        int rnd = scene_number;
        while(scene_number == rnd)
        {
            rnd = Random.Range(1,4);
        }
            
        StopAllCoroutines();
        if(restart)
        {
            StartCoroutine(end_Coroutine(int.Parse(SceneManager.GetActiveScene().name.Substring(SceneManager.GetActiveScene().name.Length - 1))));
        }
        else
        {
            StartCoroutine(end_Coroutine(rnd));
        }
            
    }
    private void OnGUI() 
    {
        GUI.skin.label.fontSize = (int)olcek;
        
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        
        if(touched == false)
        {
            Vector2 size = centeredStyle.CalcSize (new GUIContent ("TAP TO PLAY"));
            GUI.Label(new Rect(Screen.width/2-(size.x/2), Screen.height/2-(olcek*2), size.x, size.y), "TAP TO PLAY");
        }

        if (GUI.Button(new Rect(Screen.width-(olcek) ,0 ,olcek, olcek), btnTexture))
        {
            //replay
            sifirla();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void sifirla()
    {
        touched = false;
        puan = 0;
        end_wall_colli = false;
        point_y = 0.5f;
        start_d_Coroutine = false;
        start_a_Coroutine = false;
        start_s_Coroutine = false;
        start_w_Coroutine = false;
    }
}
