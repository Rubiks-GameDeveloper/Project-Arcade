using UnityEngine;

namespace UIScripts
{
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("ForButton");
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("ForButton");
            }
        }
    }
}