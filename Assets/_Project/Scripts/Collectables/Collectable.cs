using UnityEngine;

namespace _Project.Scripts.Collectables
{
    [RequireComponent(typeof(Animator))]
    public abstract class Collectable : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int PickUp = Animator.StringToHash("PickUp");
        
        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // TODO: Test Collectable
            if (!other.CompareTag("Player")) return;
            _animator.SetTrigger(PickUp);
            Debug.Log("Object Picked Up: " + gameObject.name + " by " + other.gameObject.name);
            ObjectCollected(other);
        }

        protected abstract void ObjectCollected(Collider other);
    }
}