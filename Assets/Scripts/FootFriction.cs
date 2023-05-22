using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootFriction : MonoBehaviour
{
    [SerializeField] private PhysicMaterial defaultFriction;
    [SerializeField] private PhysicMaterial zeroFriction;

    [SerializeField] private PhysicMaterial middleFriction;


    [SerializeField] private Collider fr_foot;  //firtst right
    [SerializeField] private Collider fl_foot;  //firtst left

    [SerializeField] private Collider sr_foot;
    [SerializeField] private Collider sl_foot;

    [SerializeField] private Collider tr_foot;
    [SerializeField] private Collider tl_foot;

    public void SetForwardFriction(){
        fr_foot.material = zeroFriction;
        tr_foot.material = zeroFriction;
        sl_foot.material = zeroFriction;

        fl_foot.material = defaultFriction;
        sr_foot.material = defaultFriction;
        tl_foot.material = defaultFriction;
    }

    public void SetBackwardFriction(){
        fr_foot.material = defaultFriction;
        tr_foot.material = defaultFriction;
        sl_foot.material = defaultFriction;

        fl_foot.material = zeroFriction;
        sr_foot.material = zeroFriction;
        tl_foot.material = zeroFriction;
    }

    public void SetIdleFriction(){
        fr_foot.material = middleFriction;
        tr_foot.material = middleFriction;
        sl_foot.material = middleFriction;

        fl_foot.material = middleFriction;
        sr_foot.material = middleFriction;
        tl_foot.material = middleFriction;
    }
}
