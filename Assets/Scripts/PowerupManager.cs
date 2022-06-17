using System.Collections;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private float _powerupDropTime = 2.0f;
    private bool _stopPowerups = false;

    void Start()
    {
        StartCoroutine(PowerupSpawnRoutine());
    }
    void Update()
    {

    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(_stopPowerups == false)
        {
            GameObject newTripleShotPowerup = Instantiate(_tripleShotPowerupPrefab, transform.position + new Vector3(Random.Range(-10, 10), 7, 0), Quaternion.identity);

            newTripleShotPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }
    public void SetPowerupSpawn()
    {
        _stopPowerups = true;
    }
}
