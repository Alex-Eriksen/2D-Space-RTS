using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemViewCameraTextureScript : MonoBehaviour
{
    public RenderTexture SystemViewTexture;

    public Camera SystemCamera;
    public Camera UICamera;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        SystemViewTexture = new RenderTexture( (int)rectTransform.rect.width, (int)rectTransform.rect.height, 24 );
        SystemCamera.targetTexture = SystemViewTexture;
        UICamera.targetTexture = SystemCamera.targetTexture;
        //SystemCamera.aspect = rectTransform.rect.width / rectTransform.rect.height;
        //SystemCamera.transform.localScale = new Vector3(rectTransform.rect.width, rectTransform.rect.height, 1);

        GetComponent<RawImage>().texture = SystemViewTexture;
    }
}
