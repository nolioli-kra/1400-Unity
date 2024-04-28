using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract void Activate();
    public abstract bool CanActivate();
}

public class DashAbility : Ability
{
    public override void Activate()
    {

        Debug.Log("Dash activated");
    }

    public override bool CanActivate()
    {
        return true;
    }
}
