using UnityEngine;

public class BossHitBoxAttack : MonoBehaviour
{
    public GameObject attackHitbox;  

    void Start()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    public void EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }
}
