using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEventAnimationReceiver : MonoBehaviour
{
    public CharacterCombat combat;
    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }
}
