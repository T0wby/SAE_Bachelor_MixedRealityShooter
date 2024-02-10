using System;
using System.Collections;
using Oculus.Interaction;
using Player;
using UnityEngine;

namespace Items
{
    public class HealMushroom : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private int _healAmount = 10;
        [SerializeField] private PlaceableVRItem _selfPlaceable;
        [Header("GrabEvent")] 
        [SerializeField] protected GrabInteractable _grabInteractable;

        private PlayerStatus _player;

        private void Awake()
        {
            _grabInteractable.WhenSelectingInteractorRemoved.Action += OnReleased;
        }

        private void OnReleased(GrabInteractor interactor)
        {
            HealPlayer();
        }

        private void HealPlayer()
        {
            _player = FindObjectOfType<PlayerStatus>();
            _player.HealPlayer(_healAmount);
            StartCoroutine(ReturnShroom());
        }
        
        IEnumerator ReturnShroom()
        {
            yield return new WaitForSeconds(2.0f);
            _selfPlaceable.ReturnThisToPool();
            yield return null;
        }
    }
}