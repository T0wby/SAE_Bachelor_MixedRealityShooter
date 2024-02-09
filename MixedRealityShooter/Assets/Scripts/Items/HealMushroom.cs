using System;
using Oculus.Interaction;
using Player;
using UnityEngine;

namespace Items
{
    public class HealMushroom : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private int _healAmount = 10;
        [Header("GrabEvent")] 
        [SerializeField] protected GrabInteractable _grabInteractable;

        private PlayerStatus _player;

        private void Awake()
        {
            _grabInteractable.WhenSelectingInteractorAdded.Action += OnGrabbed;
        }

        private void OnGrabbed(GrabInteractor interactor)
        {
            HealPlayer();
        }

        private void HealPlayer()
        {
            _player = FindObjectOfType<PlayerStatus>();
            _player.HealPlayer(_healAmount);
        }
    }
}