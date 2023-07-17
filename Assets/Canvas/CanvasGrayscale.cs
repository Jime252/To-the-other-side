using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGrayscale : MonoBehaviour
{
    [SerializeField] Material grayscaleMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, grayscaleMaterial);
    }
}
