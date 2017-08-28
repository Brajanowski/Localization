using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationTest : MonoBehaviour {
  void Start() {
    Debug.Log(Localization.instance.GetString("Localization test 1"));
    Debug.Log(Localization.instance.GetString("Localization test 2"));
    Debug.Log(Localization.instance.GetString("Localization test 3"));
    Debug.Log(Localization.instance.GetString("Localization test 4 - only in default"));

    Localization.instance.LoadLocalization("pl");
    Debug.Log(Localization.instance.GetString("Localization test 1"));
    Debug.Log(Localization.instance.GetString("Localization test 2"));
    Debug.Log(Localization.instance.GetString("Localization test 3"));
    Debug.Log(Localization.instance.GetString("Localization test 4 - only in default"));
  }
}
