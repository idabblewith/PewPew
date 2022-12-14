using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader")]
public class InputReader : ScriptableObject, 
    Controls.IGameplayActions, Controls.IPauseMenuActions, Controls.IMainMenuActions
{
    #region  Initialisation & InputControls ====================================================
    private Controls m_Controls;

    private void Awake()
    {
        // if(m_Controls == null)
        // {
        //     m_Controls = new Controls();
        //     m_Controls.MainMenu.SetCallbacks(this);
        //     m_Controls.Gameplay.SetCallbacks(this);
        //     m_Controls.PauseMenu.SetCallbacks(this);
        // }
        // m_Controls.Gameplay.Movement.performed += ctx => m_HorizontalInput = ctx.ReadValue<Vector2>();
    }

    private void OnEnable() 
    {
        if(m_Controls == null)
        {
            m_Controls = new Controls();
            m_Controls.MainMenu.SetCallbacks(this);
            m_Controls.Gameplay.SetCallbacks(this);
            m_Controls.PauseMenu.SetCallbacks(this);
        }
        m_Controls.Gameplay.Enable();
    }

    public void WhatIsEnabled()
    {
        Debug.Log($"MM Controls Enabled: {m_Controls.MainMenu.enabled}");
        Debug.Log($"Gameplay Controls Enabled: {m_Controls.Gameplay.enabled}");
        Debug.Log($"Pause Controls Enabled: {m_Controls.PauseMenu.enabled}");
    }

    private void OnDisable() 
    {
        DisableAllInput();
    }

    public void EnableMenuInput()
    {
        m_Controls.Gameplay.Disable();
        m_Controls.PauseMenu.Disable();
        m_Controls.MainMenu.Enable();
    }

    public void EnablePauseMenuInput()
    {
        if (m_Controls.MainMenu.enabled)
        {
            m_Controls.MainMenu.Disable();
        }
        m_Controls.Gameplay.Disable();
        m_Controls.PauseMenu.Enable();
    }

    public void EnableGameplayInput()
    {
        if (m_Controls.MainMenu.enabled)
        {
            m_Controls.MainMenu.Disable();
        }
        m_Controls.PauseMenu.Disable();
        m_Controls.Gameplay.Enable();
    }

    private void DisableAllInput()
    {
        m_Controls.Gameplay.Disable();
        m_Controls.MainMenu.Disable();
        m_Controls.PauseMenu.Disable();
    }
    #endregion

    #region Events ====================================================
    public Vector2 MouseMovement;

    
    public event Action paused;
    public event Action unpaused;
    public event Action<Vector2> Movement;
    public event Action<Vector2> OnMouseMovement;
    public event Action JumpEvent;
    public event Action SprintEvent;
    public event Action SprintReleasedEvent;
    #endregion
    
    #region InGame - Interface Implementation ====================================================
    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed) 
        {
            paused?.Invoke();
        }
    }

    // Looking
    public void OnMouseX(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseMovement.x = context.ReadValue<float>();
            OnMouseMovement?.Invoke(MouseMovement);
        }
    }

    public void OnMouseY(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MouseMovement.y = context.ReadValue<float>();
            OnMouseMovement?.Invoke(MouseMovement);
        }
    }

    // Moving
    public void OnMovement(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Movement?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SprintEvent?.Invoke();
        }
    }

    public void OnSprintReleased(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SprintReleasedEvent?.Invoke();
        }
    }

    // Jumping
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpEvent?.Invoke();
        }
    }





    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnGrenade(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }



    public void OnKnife(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }



    public void OnReload(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }



    public void OnZoom(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    #endregion

    #region MainMenu - Interface Implementation ====================================================
    public void OnMainMenuAccept(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnMainMenuCancel(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnMainMenuNavigation(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }
    #endregion

    #region PauseMenu - Interface Implementation ====================================================
    public void OnPauseMenuAccept(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnPauseMenuCancel(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }

    public void OnPauseMenuNavigation(InputAction.CallbackContext context)
    {
        // throw new NotImplementedException();
    }
    public void OnUnpause(InputAction.CallbackContext context)
    {
        if(context.performed) 
        {
            // Debug.Log("Input Reader: Unpaused");
            unpaused?.Invoke();
        }
    }

    #endregion




}