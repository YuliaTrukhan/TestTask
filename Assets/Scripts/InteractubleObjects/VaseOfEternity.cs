
using UnityEngine;

public class VaseOfEternity : BaseObject, IHittable
{
    public override ObjectType Type => ObjectType.Vase;

    public void TakeDamage()
    {
        ActionCompleted();
    }
}
