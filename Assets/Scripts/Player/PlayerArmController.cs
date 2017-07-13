using UnityEngine;

public class PlayerArmController : MonoBehaviour
{
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Running(){
        animator.SetFloat("Run", 1.0f);
    }

    public void Walking(){
        animator.SetFloat("Run", 0.5f);
    }

    public void Attack(){
        int rd = Random.Range(1, 4);
        animator.SetTrigger("Attack " + rd.ToString());
    }
}