using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.Instance.OnMonsterAnimation += PlayActionAnim;
    }

    public void PlayActionAnim(string _trigger)
    {
        animator?.SetTrigger(_trigger);
    }
}
