using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StickCounter : MonoBehaviour
{
    public static int collectedSticks = 0;

     public static bool TrySpendSticks(int amount)
    {
        if (collectedSticks >= amount)
        {
            collectedSticks -= amount;
            return true;
        }
        return false;
    }
    public static void AddStick()
    {
        collectedSticks++;
        Debug.Log("Sticks collected: " + collectedSticks);
    }
}