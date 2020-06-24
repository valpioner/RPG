using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replacableAttackAnimation;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float locomotionAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }

        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude /*speed*/ / agent.speed /*max speed*/;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        //Debug.Log("animator: " + animator);
        //Debug.Log("speedPercent: " + speedPercent);

        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replacableAttackAnimation.name] = currentAttackAnimSet[attackIndex];
    }
}
