using UnityEngine;

public class AnimatedSprites : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    public int frame;


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    public void Animate()
    {
        frame++;

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(Animate), 1f / GameManager.instance.gameSpeed);
    }
}
