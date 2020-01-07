using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitMenu : MonoBehaviour
{
     GameObject[] exitMenuOptions;
     int backButtonCount;

    // Start is called before the first frame update
    void Start()
    {
          //Hides menu buttons 
          exitMenuOptions = GameObject.FindGameObjectsWithTag("ExitMenu");
          HideExitMenu();
    }

    // Update is called once per frame
    void Update()
    {
          //Single back button click for exit menu. Double back button click to exit immediately.
          if (Input.GetKeyDown(KeyCode.Escape))
          {
               if (exitMenuOptions[0].activeSelf == false)
               {
                    ShowExitMenu();
               }
               else
               {
                    Application.Quit();
               }
          }
    }

     public void QuitGame()
     {
          Application.Quit();
     }

     public void ResumeGame()
     {
          //Does nothing and re-hides buttons on resume
          for (int i = 0; i < exitMenuOptions.Length; i++)
          {
               exitMenuOptions[i].SetActive(false);
          }
     }
     void HideExitMenu()
     {
          //Hides exit menu options
          for (int i = 0; i < exitMenuOptions.Length; i++)
          {
               exitMenuOptions[i].SetActive(false);
          }
     }

     void ShowExitMenu()
     {
          //Shows the exit menu buttons
          for (int i = 0; i < exitMenuOptions.Length; i++)
          {
               exitMenuOptions[i].SetActive(true);
          }
     }
}
