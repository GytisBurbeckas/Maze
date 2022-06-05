using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public string SceneName;
    public GameObject gameMainMenuButton;
    public GameObject winMainMenuButton;
    public ParticleSystem winParticles;
    private void OnTriggerEnter(Collider collision)
    {
        SceneManager.LoadScene(SceneName);
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            gameMainMenuButton.SetActive(false);
            winMainMenuButton.SetActive(true);
            winParticles.Play();
        }
    }

}
