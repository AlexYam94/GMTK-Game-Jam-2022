using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{

    private bool _canInventoryPill;
    private PlayerController _playerController;
    private PlayerPillController _playerPillController;
    private int _inventorySizeupPillNum;
    private int _inventorySizedownPillNum;
    private int _inventoryAcceleratePillNum;
    private int _inventoryDeceleratePillNum;
    private int _inventoryInvinciblePillNum;
    private int _inventoryInvisiblePillNum;
    private Pill _currentPill;

    public int _maxInventoryToPill;
    public int _maxInventorySizeupPill;
    public int _maxInventorySizedownPill;
    public int _maxInventoryAcceleratePill;
    public int _maxInventoryDeceleratePill;
    public int _maxInventoryInvinciblePill;
    public int _maxInventoryInvisiblePill;

    public Image _sizeupImage;
    public Image _sizedownImage;
    public Image _accelerateImage;
    public Image _deceeratImage;
    public Image _invincibleImage;
    public Image _invisibleImage;

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
            if (Input.GetKeyDown(KeyCode.Alpha1) && (_inventorySizeupPillNum >= 1))
            {
                RemovePill(PlayerPillController.PillType.Sizeup);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && (_inventorySizedownPillNum >= 1))
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
            if (Input.GetKeyDown(KeyCode.Alpha6) && (_inventoryInvisiblePillNum >= 1))
            {
                RemovePill(PlayerPillController.PillType.Invisible);
            }
        }
    }

    public bool CanInventoryPill(Pill pill)
    {
        _currentPill = pill;
        if ((_inventorySizeupPillNum + _inventorySizedownPillNum + _inventoryInvinciblePillNum + _inventoryAcceleratePillNum + _inventoryDeceleratePillNum) >= _maxInventoryToPill)
        {
            _canInventoryPill = false;
            return _canInventoryPill;
        }
        switch (_currentPill.type)
        {
            case PlayerPillController.PillType.Sizeup:
                _canInventoryPill = (_inventorySizeupPillNum < _maxInventorySizeupPill);
                return _canInventoryPill;
            case PlayerPillController.PillType.Sizedown:
                _canInventoryPill = (_inventorySizedownPillNum < _maxInventorySizeupPill);
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
            case PlayerPillController.PillType.Invisible:
                _canInventoryPill = (_inventoryInvisiblePillNum < _maxInventoryInvisiblePill);
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
                if (_inventorySizeupPillNum <= _maxInventorySizeupPill)
                {
                    _inventorySizeupPillNum += 1;
                    _sizeupImage.gameObject.SetActive(true);
                }
                break;
            case PlayerPillController.PillType.Sizedown:
                if (_inventorySizedownPillNum <= _maxInventorySizedownPill)
                {
                    _inventorySizedownPillNum += 1;
                    _sizedownImage.gameObject.SetActive(true);
                }
                break;
            case PlayerPillController.PillType.Invincible:
                if (_inventoryInvinciblePillNum <= _maxInventoryInvinciblePill)
                {
                    _inventoryInvinciblePillNum += 1;
                    _invincibleImage.gameObject.SetActive(true);
                }
                break;
            case PlayerPillController.PillType.Accelerate:
                if (_inventoryAcceleratePillNum <= _maxInventoryAcceleratePill)
                {
                    _inventoryAcceleratePillNum += 1;
                    _accelerateImage.gameObject.SetActive(true);
                }
                break;
            case PlayerPillController.PillType.Decelerate:
                if (_inventoryDeceleratePillNum <= _maxInventoryDeceleratePill)
                {
                    _inventoryDeceleratePillNum += 1;
                    _deceeratImage.gameObject.SetActive(true);
                }
                break;
            case PlayerPillController.PillType.Invisible:
                if (_inventoryInvisiblePillNum <= _maxInventoryInvinciblePill)
                {
                    _inventoryInvisiblePillNum += 1;
                    _invisibleImage.gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void ResetInventory()
    {
        _inventorySizeupPillNum--;
        _sizeupImage.gameObject.SetActive(false);

        _inventorySizedownPillNum--;
        _sizedownImage.gameObject.SetActive(false);

        _inventoryInvinciblePillNum--;
        _invincibleImage.gameObject.SetActive(false);

        _inventoryAcceleratePillNum--;
        _accelerateImage.gameObject.SetActive(false);

        _inventoryDeceleratePillNum--;
        _deceeratImage.gameObject.SetActive(false);

        _inventoryInvisiblePillNum--;
        _invisibleImage.gameObject.SetActive(false);

        _canInventoryPill = true;
    }

    private void RemovePill(PlayerPillController.PillType type) {
        switch (type)
        {
            case PlayerPillController.PillType.Sizeup:
                _currentPill = GetSizeupPill();
                _inventorySizeupPillNum--;
                _sizeupImage.gameObject.SetActive(false);
                break;
            case PlayerPillController.PillType.Sizedown:
                _currentPill = GetSizedownPill();
                _inventorySizedownPillNum--;
                _sizedownImage.gameObject.SetActive(false);
                break;
            case PlayerPillController.PillType.Invincible:
                _currentPill = GetInvinciblePill();
                _inventoryInvinciblePillNum--;
                _invincibleImage.gameObject.SetActive(false);
                break;
            case PlayerPillController.PillType.Accelerate:
                _currentPill = GetAcceleratePill();
                _inventoryAcceleratePillNum--;
                _accelerateImage.gameObject.SetActive(false);
                break;
            case PlayerPillController.PillType.Decelerate:
                _currentPill = GetDeceleratePill();
                _inventoryDeceleratePillNum--;
                _deceeratImage.gameObject.SetActive(false);
                break;
            case PlayerPillController.PillType.Invisible:
                _currentPill = GetInvisiblePill();
                _inventoryInvisiblePillNum--;
                _invisibleImage.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        _playerPillController.TakePill(_currentPill, type);
        _canInventoryPill = true;
    }

    private Pill GetSizeupPill()
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
            targetColor = new Color(255f, 162f, 0f, 255f),
            targetInvincibleTime = 0,
            paralyzeTime = 0,
            controlInverted = false,
            canStop = true,
        };
    }

    private Pill GetSizedownPill()
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
            targetSpeedScale = .6f,
            targetColor = new Color(255f, 255f, 0f, 255f),
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
            targetHPScale = 3,
            targetSpeedScale = .6f,
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
            targetColor = new Color(0, 0, 0, 255),
            targetInvincibleTime = 5,
            paralyzeTime = 0,
            controlInverted = true,
            canStop = true,
        };
    }
    private Pill GetInvisiblePill()
    {
        return new Pill
        {
            type = PlayerPillController.PillType.Invisible,
            duration = 5,
            startFlashTime = 2,
            flashInterval = .3f,
            targetXScale = 1,
            targetYScale = 1,
            targetJumpForceScale = 1,
            targetHPScale = 1,
            targetSpeedScale = 1f,
            targetColor = new Color(255, 255, 255, 0),
            targetInvincibleTime = 0,
            paralyzeTime = 0,
            controlInverted = false,
            canStop = true,
        };
    }
}
