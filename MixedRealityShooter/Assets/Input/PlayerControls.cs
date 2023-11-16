//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""10adf65d-3b5f-4d92-970c-a7b50eac6ee9"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""94c259a8-c77f-46ab-8876-cad1ecbede92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateAndScale"",
                    ""type"": ""Value"",
                    ""id"": ""bbc274d9-5eec-4318-82e0-2611bd29f952"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SwitchRotScale"",
                    ""type"": ""Button"",
                    ""id"": ""ec6d25cf-1a3c-413b-9f1c-cecf331b77a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlaceObj"",
                    ""type"": ""Button"",
                    ""id"": ""270daba7-a55e-4435-8561-cb9bd17f5765"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FireWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""39964867-6e3b-4e8c-a36c-034ce9038183"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ResetView"",
                    ""type"": ""Button"",
                    ""id"": ""59b97409-e6e4-48b8-bb7b-9387e76bbea0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fed665f3-1da7-4b3b-9865-d64d7df120d2"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0656d94d-7825-4416-a6e7-291b073b0e4b"",
                    ""path"": ""<XRController>{RightHand}/thumbstick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateAndScale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28407a08-8e95-4a0e-b7da-96af3697f2a4"",
                    ""path"": ""<XRController>{RightHand}/joystickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchRotScale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df0bc007-ee77-43a9-947e-cd570a6a489e"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cae6cea-c38a-4b73-a17c-7ea94a4788c9"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11d973a9-ae89-4eff-a063-0c01a7965a2a"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e9028b7-e9b6-4566-9b29-8321ed4300a6"",
                    ""path"": ""<XRController>{RightHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_RotateAndScale = m_Player.FindAction("RotateAndScale", throwIfNotFound: true);
        m_Player_SwitchRotScale = m_Player.FindAction("SwitchRotScale", throwIfNotFound: true);
        m_Player_PlaceObj = m_Player.FindAction("PlaceObj", throwIfNotFound: true);
        m_Player_FireWeapon = m_Player.FindAction("FireWeapon", throwIfNotFound: true);
        m_Player_ResetView = m_Player.FindAction("ResetView", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_RotateAndScale;
    private readonly InputAction m_Player_SwitchRotScale;
    private readonly InputAction m_Player_PlaceObj;
    private readonly InputAction m_Player_FireWeapon;
    private readonly InputAction m_Player_ResetView;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @RotateAndScale => m_Wrapper.m_Player_RotateAndScale;
        public InputAction @SwitchRotScale => m_Wrapper.m_Player_SwitchRotScale;
        public InputAction @PlaceObj => m_Wrapper.m_Player_PlaceObj;
        public InputAction @FireWeapon => m_Wrapper.m_Player_FireWeapon;
        public InputAction @ResetView => m_Wrapper.m_Player_ResetView;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @RotateAndScale.started += instance.OnRotateAndScale;
            @RotateAndScale.performed += instance.OnRotateAndScale;
            @RotateAndScale.canceled += instance.OnRotateAndScale;
            @SwitchRotScale.started += instance.OnSwitchRotScale;
            @SwitchRotScale.performed += instance.OnSwitchRotScale;
            @SwitchRotScale.canceled += instance.OnSwitchRotScale;
            @PlaceObj.started += instance.OnPlaceObj;
            @PlaceObj.performed += instance.OnPlaceObj;
            @PlaceObj.canceled += instance.OnPlaceObj;
            @FireWeapon.started += instance.OnFireWeapon;
            @FireWeapon.performed += instance.OnFireWeapon;
            @FireWeapon.canceled += instance.OnFireWeapon;
            @ResetView.started += instance.OnResetView;
            @ResetView.performed += instance.OnResetView;
            @ResetView.canceled += instance.OnResetView;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @RotateAndScale.started -= instance.OnRotateAndScale;
            @RotateAndScale.performed -= instance.OnRotateAndScale;
            @RotateAndScale.canceled -= instance.OnRotateAndScale;
            @SwitchRotScale.started -= instance.OnSwitchRotScale;
            @SwitchRotScale.performed -= instance.OnSwitchRotScale;
            @SwitchRotScale.canceled -= instance.OnSwitchRotScale;
            @PlaceObj.started -= instance.OnPlaceObj;
            @PlaceObj.performed -= instance.OnPlaceObj;
            @PlaceObj.canceled -= instance.OnPlaceObj;
            @FireWeapon.started -= instance.OnFireWeapon;
            @FireWeapon.performed -= instance.OnFireWeapon;
            @FireWeapon.canceled -= instance.OnFireWeapon;
            @ResetView.started -= instance.OnResetView;
            @ResetView.performed -= instance.OnResetView;
            @ResetView.canceled -= instance.OnResetView;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnRotateAndScale(InputAction.CallbackContext context);
        void OnSwitchRotScale(InputAction.CallbackContext context);
        void OnPlaceObj(InputAction.CallbackContext context);
        void OnFireWeapon(InputAction.CallbackContext context);
        void OnResetView(InputAction.CallbackContext context);
    }
}
