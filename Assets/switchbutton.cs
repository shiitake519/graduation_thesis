using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchbutton : MonoBehaviour
{
    public GameObject gameobjectei;
    public GameObject gameobjectwo;
    public GameObject gameobjectshi;
    public GameObject gameobjectmigi;
    public GameObject sphere;
    //int num = 1;
    //public switchbutton button;
    public ray r;
    public bool onbutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onbutton = false;
    }
    void OnGUI()
    {
        if(GUI.Button (new Rect (10,10,100,40), "ei"))
        {
            gameobjectei.SetActive (true);
            gameobjectwo.SetActive (false);
            gameobjectshi.SetActive (false);
            gameobjectmigi.SetActive (false);
            r.num = 1;
            r.count = 0;
            r.fcount = 0;
            onbutton = true;
            sphere.SetActive(true);
        }
        if(GUI.Button (new Rect (10,50,100,40), "wo"))
        {
            gameobjectei.SetActive (false);
            gameobjectwo.SetActive (true);
            gameobjectshi.SetActive (false);
            gameobjectmigi.SetActive (false);
            r.num = 2;
            r.count = 0;
            r.fcount = 0;
            onbutton = true;
            sphere.SetActive(true);
        }
        if(GUI.Button (new Rect (10,90,100,40), "shi"))
        {
            gameobjectei.SetActive (false);
            gameobjectwo.SetActive (false);
            gameobjectshi.SetActive (true);
            gameobjectmigi.SetActive (false);
            r.num = 3;
            r.count = 0;
            r.fcount = 0;
            onbutton = true;
            sphere.SetActive(true);
        }
        if(GUI.Button (new Rect (10,130,100,40), "migi"))
        {
            gameobjectei.SetActive (false);
            gameobjectwo.SetActive (false);
            gameobjectshi.SetActive (false);
            gameobjectmigi.SetActive (true);
            r.num = 4;
            r.count = 0;
            r.fcount = 0;
            onbutton = true;
            sphere.SetActive(true);
        }
    }
}
