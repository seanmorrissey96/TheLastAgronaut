using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public CanvasGroup canvasGroupCrafting;
    public Camera camera;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            if(canvasGroupCrafting.alpha == 0)
            {
                canvasGroupCrafting.alpha = 1;
                canvasGroupCrafting.blocksRaycasts = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                camera.clearFlags = CameraClearFlags.Nothing;
                camera.cullingMask = 0;
            }
            else
            {
                canvasGroupCrafting.alpha = 0;
                canvasGroupCrafting.blocksRaycasts = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                camera.clearFlags = CameraClearFlags.Skybox;
                camera.cullingMask = 1;
            }
        }
    }
}
