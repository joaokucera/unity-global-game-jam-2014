using UnityEngine;
using System.Collections;

[AddComponentMenu("Tools / Scripts / Animation Sprite")]
public class AnimationSprite : MonoBehaviour
{
    public Renderer graphicRenderer;

    public void AnimateSprite(int columnSize, int rowSize, int columnFrameStart, int rowFrameStart, int totalFrames, int framesPerSecond)
    {
        int index = (int)(Time.time * framesPerSecond);
        index = index % totalFrames;

        Vector2 size = new Vector2(1f / columnSize, 1f / rowSize);

        int u = index % columnSize;
        int v = index / columnSize;

        Vector2 offset = new Vector2((u + columnFrameStart) * size.x, (1f - size.y) - (v + rowFrameStart) * size.y);

        graphicRenderer.material.mainTextureOffset = offset;
        graphicRenderer.material.mainTextureScale = size;

        //renderer.material.SetTextureOffset("_BumpMap", offset);
        //renderer.material.SetTextureScale("_BumpMap", size);
    }
}
