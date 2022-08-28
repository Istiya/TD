using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelItemController : MonoBehaviour
{
    ShopPanelItemModel _model;
    Button _button;

    private void Awake()
    {
        _model = GetComponent<ShopPanelItemModel>();
        _button = GetComponent<Button>();

        GameManager.instance.OnMoneyChange += ChangeActive;
    }

    public void OnSelect()
    {
        if (!_button.colors.Equals(_model.disabled))
        {
            _button.colors = _model.selected;
        }
    }

    public void OnUnselect()
    {
        if (!_button.colors.Equals(_model.disabled))
        {
            _button.colors = _model.defaultCB;
        }
    }

    private void ChangeActive(int money)
    {
        if(money >= (int)_model.cost)
        {
            if (_button.colors.Equals(_model.disabled))
            {
                _button.colors = _model.defaultCB;
            }
        }
        else
        {
            _button.colors = _model.disabled;
        }
    }

}
