using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    Model _model;
    Vector3 simulatedVector;
    Controls virtualStick;

    public Controller(Model model, Vector3 SV, Controls controls)
    { _model = model;
        simulatedVector = SV;
        virtualStick = controls;
    }

      public void FixedUpdate()
      {
            simulatedVector.x = virtualStick.value.x;
            simulatedVector.z = virtualStick.value.y;

            _model.Movement(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

      }

    public IEnumerator Stoper()
    {
        yield return new WaitForSeconds(3);
        FixedUpdate();
    }
  

    public void OnUpdate()
    { }
}
