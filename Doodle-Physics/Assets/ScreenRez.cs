using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRez : MonoBehaviour
{
     public Resolution displayRez;

     // Start is called before the first frame update
     // Sets the game resolution to match the resolution of the phone for canvas/background/collider match 
     void Start()
     {
          GameObject myCanvas = GameObject.Find("Canvas");
          displayRez = Screen.currentResolution;
          //myCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(displayRez.width, displayRez.height);
          myCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(displayRez.width, displayRez.height);


     }
}
