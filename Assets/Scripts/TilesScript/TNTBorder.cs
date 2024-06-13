using UnityEngine;

namespace TilesScript
{
    public class TNTBorder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player") || other.CompareTag("Detector")) return;
            transform.position = other.transform.position;
        }
    }
}
