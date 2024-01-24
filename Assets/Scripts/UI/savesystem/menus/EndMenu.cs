using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
   public void ReturnToTitle()//function to return to title screen
   {
      SceneManager.LoadScene(0);//load title screen
   }
}
