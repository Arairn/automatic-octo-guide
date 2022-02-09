using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public new string name;
    public int manaCost;
    public int specialCost;

    public bool targetShouldBeSelected;
    public TargetsAvialable targetsAvialable;
    public AttackEffect visualEffectOnTarget;
    public AttackEffect visualEffectOnSelf;

    public List<NewBuffBit> bit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyMove(CharacterFacade currentCharFacade, CharacterFacade target)
    {

        foreach (var item in bit)
        {
            item.EffectStart(currentCharFacade, target);
        }
        DisplayMove(this.transform, target.transform);
    }
void DisplayMove(Transform self, Transform target)
    {

        if (visualEffectOnTarget)
        {

            Instantiate(visualEffectOnTarget, target.position, target.rotation);
        }
        if (visualEffectOnSelf)
        {

            Instantiate(visualEffectOnSelf, self.position, self.rotation);
        }
        else
        {
            if (LogController.instance.BattleAnimationsLog)
            {
                Debug.Log("BattleMove не имеет анимации применения");
            }
        }
    }
}
