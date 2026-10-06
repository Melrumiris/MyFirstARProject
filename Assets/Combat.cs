using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Fighters")]
    public Transform fighterBow;
    public Animator animatorBow;

    public Transform fighterSword;
    public Animator animatorSword;

    public float attackRange = 0.25f;

    void Update()
    {
        if (fighterBow.gameObject.activeInHierarchy && fighterSword.gameObject.activeInHierarchy)
        {
            float distance = Vector3.Distance(fighterBow.position, fighterSword.position);
            bool inStrikeRange = distance <= attackRange;

            animatorBow.SetBool("isAttacking", inStrikeRange);
            animatorSword.SetBool("isAttacking", inStrikeRange);
        }
    }
}
