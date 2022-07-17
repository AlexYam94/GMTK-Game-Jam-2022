using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{

    private bool _canInventoryPill;
    private PlayerController _playerController;
    private PlayerPillController _playerPillController;
    private int _inventoryEnlargePillNum;
    private int _inventoryDelargePillNum;
    private int _inventoryAcceleratePillNum;
    private int _inventoryDeceleratePillNum;
    private int _inventoryInvinciblePillNum;
    private Pill _currentPill;

    public int _maxInventoryTotPill;
    public int _maxInventoryEnlargePill;
    public int _maxInventoryDelargePill;
    public int _maxInventoryAcceleratePill;
    public int _maxInventoryDeceleratePill;
    public int _maxInventoryInvinciblePill;

    // Start is called before the first frame update
    void Start()
    {
        _canInventoryPill = false;
        _playerController = GetComponent<PlayerController>();
        _playerPillController = GetComponent<PlayerPillController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerPillController.CanTakePill())
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && (_inventoryEnlargePillNum >= 1))
            {
                RemovePill(PlayerPillController.PillType.Sizeup);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && (_inventoryDelargePillNum >= 1))
            {
                RemovePill(PlayerPillController.PillType.Sizedown);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && (_inventoryInvinciblePillNum >= 1))
            {
                RemovePill(PlayerPillController.PillType.Invincible);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && (_inventoryAcceleratePillNum >= 1))
            {
                RemovePill(PlayerPillController.PillType.Accelerate);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && (_inventoryDeceleratePillNum >= 1))
            {
                RemovePill(PlayerPillController.PillType.Decelerate);
            }
        }
    }

    public bool CanInventoryPill(Pill pill)
    {
        _currentPill = pill;
        if ((_inventoryEnlargePillNum + _inventoryDelargePillNum + _inventoryInvinciblePillNum + _inventoryAcceleratePillNum + _inventoryDeceleratePillNum) >= _maxInventoryTotPill)
        {
            _canInventoryPill = false;
            return _canInventoryPill;
        }
        switch (_currentPill.type)
        {
            case PlayerPillController.PillType.Sizeup:
                _canInventoryPill = (_inventoryEnlargePillNum < _maxInventoryEnlargePill);
                return _canInventoryPill;
            case PlayerPillController.PillType.Sizedown:
                _canInventoryPill = (_inventoryDelargePillNum < _maxInventoryEnlargePill);
                return _canInventoryPill;
            case PlayerPillController.PillType.Invincible:
                _canInventoryPill = (_inventoryInvinciblePillNum < _maxInventoryInvinciblePill);
                return _canInventoryPill;
            case PlayerPillController.PillType.Accelerate:
                _canInventoryPill = (_inventoryAcceleratePillNum < _maxInventoryAcceleratePill);
                return _canInventoryPill;
            case PlayerPillController.PillType.Decelerate:
                _canInventoryPill = (_inventoryDeceleratePillNum < _maxInventoryDeceleratePill);
                return _canInventoryPill;
            default:
                return _canInventoryPill;
        }
    }

    public void InventoryPill(Pill pill)
    {
        _currentPill = pill;
        switch (_currentPill.type)
        {
            case PlayerPillController.PillType.Sizeup:
                if (_inventoryEnlargePillNum <= _maxInventoryEnlargePill)
                {
                    _inventoryEnlargePillNum += 1;
                }
                break;
            case PlayerPillController.PillType.Sizedown:
                if (_inventoryDelargePillNum <= _maxInventoryDelargePill)
                {
                    _inventoryDelargePillNum += 1;
                }
                break;
            case PlayerPillController.PillType.Invincible:
                if (_inventoryInvinciblePillNum <= _maxInventoryInvinciblePill)
                {
                    _inventoryInvinciblePillNum += 1;
                }
                break;
            case PlayerPillController.PillType.Accelerate:
                if (_inventoryAcceleratePillNum <= _maxInventoryAcceleratePill)
                {
                    _inventoryAcceleratePillNum += 1;
                }
                break;
            case PlayerPillController.PillType.Decelerate:
                if (_inventoryDeceleratePillNum <= _maxInventoryDeceleratePill)
                {
                    _inventoryDeceleratePillNum += 1;
                }
                break;
            default:
                break;
        }
    }

    private void RemovePill(PlayerPillController.PillType type) {
        switch (type)
        {
            case PlayerPillController.PillType.Sizeup:
                _currentPill = GetEnlargePill();
                _inventoryEnlargePillNum--;
                break;
            case PlayerPillController.PillType.Sizedown:
                _currentPill = GetDelargePill();
                _inventoryDelargePillNum--;
                break;
            case PlayerPillController.PillType.Invincible:
                _currentPill = GetInvinciblePill();
                _inventoryInvinciblePillNum--;
                break;
            case PlayerPillController.PillType.Accelerate:
                _currentPill = GetAcceleratePill();
                _inventoryAcceleratePillNum--;
                break;
            case PlayerPillController.PillType.Decelerate:
                _currentPill = GetDeceleratePill();
                _inventoryDeceleratePillNum--;
                break;
            default:
                break;
        }
        _playerPillController.TakePill(_currentPill, type);
        _canInventoryPill = true;
    }

    private Pill GetEnlargePill()
    {
        return new Pill
        {
            type = PlayerPillController.PillType.Sizeup,
            duration = 5,
            startFlashTime = 2,
            flashInterval = .3f,
            targetXScale = 2,
            targetYScale = 2,
            targetJumpForceScale = 1.5f,
            targetHPScale = 2,
            targetSpeedScale = 1.5f,
            targetColor = new Color(255f, 126f, 0f, 255f),
            targetInvincibleTime = 0,
            paralyzeTime = 0,
            controlInverted = false,
            canStop = true,
        };
    }

    private Pill GetDelargePill()
    {
        return new Pill
        {
            type = PlayerPillController.PillType.Sizedown,
            duration = 5,
            startFlashTime = 2,
            flashInterval = .3f,
            targetXScale = .4f,
            targetYScale = .4f,
            targetJumpForceScale = .5f,
            targetHPScale = .5f,
            targetSpeedScale = .5f,
            targetColor = Color.yellow,
            targetInvincibleTime = 0,
            paralyzeTime = 0,
            controlInverted = false,
            canStop = true,
        };
    }

    private Pill GetAcceleratePill()
    {
        return new Pill
        {
            type = PlayerPillController.PillType.Accelerate,
            duration = 5,
            startFlashTime = 2,
            flashInterval = .3f,
            targetXScale = 1,
            targetYScale = 1,
            targetJumpForceScale = 1,
            targetHPScale = 1,
            targetSpeedScale = 3,
            targetColor = Color.red,
            targetInvincibleTime = 0,
            paralyzeTime = 0,
            controlInverted = false,
            canStop = false,
        };
    }

    private Pill GetDeceleratePill()
    {
        return new Pill
        {
            type = PlayerPillController.PillType.Decelerate,
            duration = 5,
            startFlashTime = 2,
            flashInterval = .3f,
            targetXScale = 1,
            targetYScale = 1,
            targetJumpForceScale = 1,
            targetHPScale = 1,
            targetSpeedScale = .5f,
            targetColor = new Color(0, 234, 255, 255),
            targetInvincibleTime = 0,
            paralyzeTime = 0,
            controlInverted = false,
            canStop = true,
        };
    }

    private Pill GetInvinciblePill()
    {
        return new Pill
        {
            type = PlayerPillController.PillType.Invincible,
            duration = 5,
            startFlashTime = 2,
            flashInterval = .3f,
            targetXScale = 1,
            targetYScale = 1,
            targetJumpForceScale = 1,
            targetHPScale = 1,
            targetSpeedScale = 1,
            targetColor = new Color(255, 255, 255, 255),
            targetInvincibleTime = 5,
            paralyzeTime = 0,
            controlInverted = true,
            canStop = true,
        };
    }
}
