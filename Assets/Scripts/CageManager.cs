using UnityEngine;

public class CageManager : MonoBehaviour
{
    public static CageManager Instance;

    private int assembledCages = 0;
    public int totalCages = 3;

    public TPShip tpShip;

    public bool allCagesAssembled { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            allCagesAssembled = PlayerPrefs.GetInt("CagesAssembled", 0) == 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NotifyCageAssembled()
    {
        assembledCages++;

        if (assembledCages >= totalCages && !allCagesAssembled)
        {
            allCagesAssembled = true;
            PlayerPrefs.SetInt("CagesAssembled", 1);
            PlayerPrefs.Save();

            if (tpShip != null)
                tpShip.TriggerUIExternally();
        }
    }
}
