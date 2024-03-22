using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene1and3 : MonoBehaviour
{
     public void GoToIntroScene()
   {
    
    SceneManager.LoadScene("IntroScene");

   }
   public void GoToControlsScene()
   {
    
    SceneManager.LoadScene("ControlsScene");

   }
}
