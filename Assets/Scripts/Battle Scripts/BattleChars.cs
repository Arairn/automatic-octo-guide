using UnityEngine;

public class BattleChars : MonoBehaviour
{

    public bool isPlayer;

    public new string name;



    public int maxHP, currentMP, maxMP, strength, defence, weaponPWR, armorPWR;
    public bool hasDied;
    public int currentHP;


    public BattleMoves[] battleMoves;
    public BattleMoves[] spawnMoves;
    public BattleMoves[] curseMoves;

    SpriteRenderer spriteRenderer;
    public Sprite AliveSprite, DeadSprite;


    float fadeSpeed = 0.5f;
    bool isFading = false;
    
    public enum CharStatus
    {
        isDead,
        isAlive
    }
    //CharStatus charStatus;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.LinkToBattleChars();
        //charStatus = CharStatus.isAlive;
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

    public void DealDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;
    }


    public void LevelUp(int level)
    {
        maxHP += 5;
        currentHP = maxHP;
        maxMP += 2;
        currentMP = maxMP;
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
                Debug.Log(name + "seems not to have dead sprite, disabling");
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

    public void DestroyIt()
    {
        Destroy(this);
    }


}
