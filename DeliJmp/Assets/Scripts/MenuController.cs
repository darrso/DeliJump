using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void PlayPressed()
    {
        int first_game = PlayerPrefs.GetInt("FirstGame", 1);
        Debug.Log(first_game);
        if(first_game == 0)
        {
            SceneLoad.Game_Load();
        }
        else
        {
            PlayerPrefs.SetInt("FirstGame", 0);
            PlayerPrefs.Save();
            SceneLoad.Tutorial_Load();
        }
    }
}
