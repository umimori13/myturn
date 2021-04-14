using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Title : MonoBehaviour
{
    public GameObject MainView;


    public void OnStartClick()
    {
        MainView = (GameObject) Instantiate(Resources.Load("Prefab/Main"));
        MainView.name = "Main";
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
