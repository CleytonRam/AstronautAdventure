using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Boss.BossBase boss;
    public bool triggerOnlyOnce = true;
    private bool hasTriggered = false; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!triggerOnlyOnce || !hasTriggered))
        {
            if (boss != null)
            {
                boss.gameObject.SetActive(true);
                boss.StartBoss();
                hasTriggered = true;
                if (triggerOnlyOnce)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("BossTrigger: Boss reference is not set.");
            }
        }
    }
}
