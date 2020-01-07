using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
     //Function to start a new game
     public void NewGame()
     {
          SceneManager.LoadScene("Game");
     }
}
