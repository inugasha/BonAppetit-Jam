//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_/Features/Input/Player_Actions.inputactions
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

public partial class @Player_Actions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player_Actions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player_Actions"",
    ""maps"": [
        {
            ""name"": ""Locomotion"",
            ""id"": ""c08301f5-f0f7-4958-a6e9-0e696c5d4a76"",
            ""actions"": [
                {
                    ""name"": ""Locomotion"",
                    ""type"": ""PassThrough"",
                    ""id"": ""90fbe9a4-3312-4368-8201-5e4eaba53d73"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aiming"",
                    ""type"": ""PassThrough"",
                    ""id"": ""06f34d77-0d21-4979-85d3-f5d99e67b513"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e0e771aa-460b-4676-8102-01c445888214"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5e07c747-456a-44a6-9534-f45d19ce2052"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fb480513-f6c5-4b07-8676-ec3ece698dc5"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4b714abd-cca7-414b-9bbd-a22a63b20c0a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""321ef125-8f81-4b0a-84a1-d7cbc7242c8a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""083982b2-7e87-4928-bffe-53d51779441f"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""95297e79-c044-4dfc-8a07-35eafd5f7ace"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""225ba388-da1f-4efe-b6c6-ae9248c2d7aa"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d496929f-19bb-49d1-8238-bdcc29418be5"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d5713977-e48d-4d75-84b2-b4230e6b6ba0"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""0432ba16-3e95-49ed-b8d4-c8c75796516f"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""204ac0ce-0494-43be-9dca-3c84b294d73d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pickup"",
                    ""type"": ""Button"",
                    ""id"": ""886779f5-9103-4b1a-bb4f-fa0e040b2e30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Launch"",
                    ""type"": ""Button"",
                    ""id"": ""c9ae044b-3261-498a-9c8b-a5d7e79f5f3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b023b56f-ea3b-4a0c-afb0-9d4e863458ca"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f04f7b1-9aab-4671-95c7-3982d2975837"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba9cafbf-be0f-4a1c-b5d8-894dce569135"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Launch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Locomotion
        m_Locomotion = asset.FindActionMap("Locomotion", throwIfNotFound: true);
        m_Locomotion_Locomotion = m_Locomotion.FindAction("Locomotion", throwIfNotFound: true);
        m_Locomotion_Aiming = m_Locomotion.FindAction("Aiming", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Fire = m_Combat.FindAction("Fire", throwIfNotFound: true);
        m_Combat_Pickup = m_Combat.FindAction("Pickup", throwIfNotFound: true);
        m_Combat_Launch = m_Combat.FindAction("Launch", throwIfNotFound: true);
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

    // Locomotion
    private readonly InputActionMap m_Locomotion;
    private List<ILocomotionActions> m_LocomotionActionsCallbackInterfaces = new List<ILocomotionActions>();
    private readonly InputAction m_Locomotion_Locomotion;
    private readonly InputAction m_Locomotion_Aiming;
    public struct LocomotionActions
    {
        private @Player_Actions m_Wrapper;
        public LocomotionActions(@Player_Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Locomotion => m_Wrapper.m_Locomotion_Locomotion;
        public InputAction @Aiming => m_Wrapper.m_Locomotion_Aiming;
        public InputActionMap Get() { return m_Wrapper.m_Locomotion; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LocomotionActions set) { return set.Get(); }
        public void AddCallbacks(ILocomotionActions instance)
        {
            if (instance == null || m_Wrapper.m_LocomotionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LocomotionActionsCallbackInterfaces.Add(instance);
            @Locomotion.started += instance.OnLocomotion;
            @Locomotion.performed += instance.OnLocomotion;
            @Locomotion.canceled += instance.OnLocomotion;
            @Aiming.started += instance.OnAiming;
            @Aiming.performed += instance.OnAiming;
            @Aiming.canceled += instance.OnAiming;
        }

        private void UnregisterCallbacks(ILocomotionActions instance)
        {
            @Locomotion.started -= instance.OnLocomotion;
            @Locomotion.performed -= instance.OnLocomotion;
            @Locomotion.canceled -= instance.OnLocomotion;
            @Aiming.started -= instance.OnAiming;
            @Aiming.performed -= instance.OnAiming;
            @Aiming.canceled -= instance.OnAiming;
        }

        public void RemoveCallbacks(ILocomotionActions instance)
        {
            if (m_Wrapper.m_LocomotionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILocomotionActions instance)
        {
            foreach (var item in m_Wrapper.m_LocomotionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LocomotionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LocomotionActions @Locomotion => new LocomotionActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private List<ICombatActions> m_CombatActionsCallbackInterfaces = new List<ICombatActions>();
    private readonly InputAction m_Combat_Fire;
    private readonly InputAction m_Combat_Pickup;
    private readonly InputAction m_Combat_Launch;
    public struct CombatActions
    {
        private @Player_Actions m_Wrapper;
        public CombatActions(@Player_Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Combat_Fire;
        public InputAction @Pickup => m_Wrapper.m_Combat_Pickup;
        public InputAction @Launch => m_Wrapper.m_Combat_Launch;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void AddCallbacks(ICombatActions instance)
        {
            if (instance == null || m_Wrapper.m_CombatActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CombatActionsCallbackInterfaces.Add(instance);
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Pickup.started += instance.OnPickup;
            @Pickup.performed += instance.OnPickup;
            @Pickup.canceled += instance.OnPickup;
            @Launch.started += instance.OnLaunch;
            @Launch.performed += instance.OnLaunch;
            @Launch.canceled += instance.OnLaunch;
        }

        private void UnregisterCallbacks(ICombatActions instance)
        {
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Pickup.started -= instance.OnPickup;
            @Pickup.performed -= instance.OnPickup;
            @Pickup.canceled -= instance.OnPickup;
            @Launch.started -= instance.OnLaunch;
            @Launch.performed -= instance.OnLaunch;
            @Launch.canceled -= instance.OnLaunch;
        }

        public void RemoveCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICombatActions instance)
        {
            foreach (var item in m_Wrapper.m_CombatActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CombatActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    public interface ILocomotionActions
    {
        void OnLocomotion(InputAction.CallbackContext context);
        void OnAiming(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
        void OnLaunch(InputAction.CallbackContext context);
    }
}
