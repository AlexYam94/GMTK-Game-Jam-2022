using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public PlayerPillController.PillType type;

    public float duration = 5;
    public float startFlashTime = 3;
    public float flashInterval = .3f;
    public float targetXScale = 1;
    public float targetYScale = 1;
    public float targetJumpForceScale = 1;
    public float targetHPScale = 1;
    public float targetSpeedScale = 1;
    public Color targetColor;
    public float targetInvincibleTime = 0;
    public float paralyzeTime = 0;
    public bool controlInverted = false;
    public bool canStop = true;

    private float _flashCounter;
    private float _effectCounter;

    private bool _interactable;
    private PlayerPillController _playerPillController;
    private PlayerInventoryController _playerInventoryController;

    private void Update()
    {
        if (_interactable)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_playerPillController.CanTakePill())
                {
                    _playerPillController.TakePill(this, type);
                    gameObject.SetActive(false);
                    Destroy(gameObject, 20f);
                }
            }
            if (Input.GetKeyDown(KeyCode.V)) {
                if (_playerInventoryController.CanInventoryPill(this))
                {
                    _playerInventoryController.InventoryPill(this);
                    gameObject.SetActive(false);
                    Destroy(gameObject, 20f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerPillController = other.GetComponentInParent<PlayerPillController>();
            _playerInventoryController = other.GetComponentInParent<PlayerInventoryController>();
            _interactable = true;
            if (_playerPillController.CanTakePill())
            {
                UIController.GetInstance().EnableInteractText();
            }
            if (_playerInventoryController.CanInventoryPill(this))
            {
                UIController.GetInstance().EnablePickupText();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerPillController = null;
            _playerInventoryController = null;
            UIController.GetInstance().DisableInteractText();
            UIController.GetInstance().DisablePickupText();
            _interactable = false;
        }
    }
}