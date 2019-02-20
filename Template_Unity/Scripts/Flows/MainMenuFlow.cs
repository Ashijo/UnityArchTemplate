using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class MainMenuFlow : Flow {
    public override void Finish() { }

    public override void FixedUpdateFlow(float _fdt) { }

    public override void InitializeFlow() { }

    public override void UpdateFlow(float _dt, InputParams _ip) {
        if (_ip.Confirm) {
            FlowManager.Instance.ChangeFlows(GV.SCENENAMES.GameScene);
        }
    }
}