using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private InputActionReference reloadInputActionReference;

    private InputAction ReloadInputAction
    {
        get
        {
            var action = reloadInputActionReference.action;
            if (!action.enabled)
            {
                action.Enable();
            }
            return action;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ReloadScene();
    }

    private void OnEnable()
    {
        ReloadInputAction.performed += OnReloadPerformed;
    }

    private void OnDisable()
    {
        ReloadInputAction.performed -= OnReloadPerformed;
    }

    private static void OnReloadPerformed(InputAction.CallbackContext ctx)
    {
        ReloadScene();
    }

    private static void ReloadScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }
}
