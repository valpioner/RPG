using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;
    CharacterCombat playerCombat;
    bool isBeingAttacked = false;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        // Attack the enemy
        playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            Debug.LogError("isBeingAttacked = true");
            isBeingAttacked = true; // Player loop attack
        }
    }
    public override void Update()
    {
        base.Update();
        if (isBeingAttacked)
        {
            playerCombat.Attack(myStats);
        }
    }

    public override void OnDefocused()
    {
        Debug.LogError("OnDefocused");
        base.OnDefocused();
        isBeingAttacked = false;
    }
}
