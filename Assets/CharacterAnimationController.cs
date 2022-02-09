using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public Sprite AliveSprite, DeadSprite;


    float fadeSpeed = 0.5f;
    bool isFading = false;

    enum CharStatus
    {
        isDead,
        isAlive
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(spriteRenderer.color.a, 0, fadeSpeed * Time.deltaTime));
            if (spriteRenderer.color.a < 0.01f)
            {
                isFading = false;
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                spriteRenderer.enabled = false;
            }
        }
    }
    public void SetSpriteToDead(bool isDead)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!spriteRenderer)

        {
            Debug.Log("SpriteRenderer is not found");
            return;
        }

        if (isDead)
        {

            if (DeadSprite)
            {
                spriteRenderer.sprite = DeadSprite;
            }
            else
            {

                //spriteRenderer.enabled = false;
                isFading = true;
                if (LogController.instance.BattleAnimationsLog)
                {
                    Debug.Log(name + "seems not to have dead sprite, disabling");
                }
            }
        }
        else
        {

            spriteRenderer.enabled = true;

            if (AliveSprite)
            {

                spriteRenderer.sprite = AliveSprite;
            }


        }

    }
}
