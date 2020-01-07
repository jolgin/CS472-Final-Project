using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
     public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))     //Back button can exit app from start screen
          {
               Application.Quit();
          }
    }

     public void QuitMenu()
     {
          Application.Quit();
     }
}
