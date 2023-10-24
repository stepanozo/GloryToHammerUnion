using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingObjects : MonoBehaviour
{






    public Vector2 startPosition;
    public Vector2 endPosition;
    public float step;
    private float progress;
    public GameObject infoCalendarObject;


    // Start is called before the first frame update
    void Start()
    {
       // startPosition = infoCalendarObject.transform.position;
       // endPosition = startPosition + new Vector2(1500f, 0);
        //infoCalendarObject.transform.position = startPosition;
    }

    // Update is called once per frame
    /*void Update()
    {

        infoCalendarObject.transform.position = Vector2(startPosition, endPosition, progress);
        progress += step;
        step *= 1.01f;
        //Короче такая шняга никуда не пойдёт.
  

    }*/
}
    