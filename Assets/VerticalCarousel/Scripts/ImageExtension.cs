using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageExtension : Image
{
    private Renderer mRender;
    private CanvasRenderer canvasRenderer;

    //I can not find properties which change the order in layer of Image component. :(
    protected override void Awake()
    {
        Debug.Log("Extension: " + fillOrigin);
    }
}
