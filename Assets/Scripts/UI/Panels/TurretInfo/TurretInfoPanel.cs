using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretInfoPanel : MonoBehaviour
{
    public static TurretInfoPanel instance;

    public Image image;
    public TMP_Text turretName;
    public TMP_Text cost;
    public TMP_Text damage;
    public TMP_Text fireRate;
    public TMP_Text DPS;
    public TMP_Text range;

    private TurretModel _model;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        if (ChoosenTurretController.instance != null)
        {
            Destroy(ChoosenTurretController.instance.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    public void Init(TurretModel model)
    {
        _model = model;
        image.sprite = _model.sprite;
        turretName.text = _model.turretName;
        cost.text += ((int)_model.cost);
        int turretDamage = PrefabManager.instance.Get(_model.projectilePrefab).GetComponent<ProjectileModel>().damage.damage * _model.firePoint.Length;
        damage.text += turretDamage.ToString();
        DPS.text += turretDamage / _model.fireRate;
        fireRate.text += _model.fireRate.ToString();
        range.text += _model.range.ToString();
    }

    public void OnSellButtonPressed()
    {
        BuildManager.instance.SellTurret(_model);
        Destroy(gameObject);
    }

    public void OnUpgradeButtonPressed()
    {
        Debug.Log("Upgraded!");
    }


    private void OnDestroy()
    {
        if (_model != null)
        {
            _model.gameObject.GetComponent<TurretController>().Unselect();
        }
    }
}
