using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Control");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
