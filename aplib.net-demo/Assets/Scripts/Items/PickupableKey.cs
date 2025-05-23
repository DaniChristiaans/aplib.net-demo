// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// Copyright Utrecht University (Department of Information and Computing Sciences)

using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Attach this class to make object pickupable.
    /// </summary>
    [RequireComponent(typeof(Key))]
    public class PickupableKey : MonoBehaviour
    {
        public event Action<Key> KeyPickedUp;

        private Key _item;

        private KeyRing _keyRing;

        public void Awake()
        {
            GameObject inventoryObject = GameObject.Find("KeyRingObject");
            _keyRing = inventoryObject.GetComponent<KeyRing>();

            if (!_keyRing) throw new UnityException("Key ring not found!");

            _item = gameObject.GetComponent<Key>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !other.material.name.Contains("PlayerPhysic"))
                return;

            KeyPickedUp?.Invoke(_item);

            _keyRing.StoreKey(_item);

            Destroy(gameObject);
        }
    }
}
