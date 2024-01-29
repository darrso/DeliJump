using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    static int menu_scene = 0;
    static int game_scene = 1;
    static int records_scene = 2;
    static int die_scene = 3;
    static int tutorial_scene = 4;
    public static void Menu_Load()
    {
        SceneManager.LoadScene(menu_scene);
    }
    public static void Game_Load()
    {
        SceneManager.LoadScene(game_scene);
    }
    public static void Records_Load()
    {
        SceneManager.LoadScene(records_scene);
    }
    public static void Die_Load()
    {
        SceneManager.LoadScene(die_scene);
    }
    public static void Tutorial_Load()
    {
        SceneManager.LoadScene(tutorial_scene);
    }
    public static void Quit()
    {
        Application.Quit();
    }
    
}
