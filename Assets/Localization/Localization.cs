using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Localization {
  public const string LOCALIZATION_PATH = "./Localization/";
  public const string DEFAULT = "default";

  // singleton
  static Localization singleton = null;
  public static Localization instance {
    get {
      if (singleton == null)
        singleton = new Localization();
      return singleton;
    }
  }

  Dictionary<string, string> defaultData = new Dictionary<string, string>();
  public Dictionary<string, string> data = new Dictionary<string, string>();

  public Localization() {
    if (!Directory.Exists(LOCALIZATION_PATH)) {
      Directory.CreateDirectory(LOCALIZATION_PATH);
    }

    // load default localization
    LoadFromFile(LOCALIZATION_PATH + DEFAULT + ".txt", out defaultData);
    LoadLocalization(DEFAULT);
  }

  public string GetString(string key) {
    string output;

    if (data.TryGetValue(key, out output)) {
      return output;
    } else if (defaultData.TryGetValue(key, out output)) {
      return output;
    }
    return "{COULDN'T FIND STRING IN LOCALIZATION DATA}";
  }

  public bool LoadLocalization(string name) {
    return LoadFromFile(LOCALIZATION_PATH + name + ".txt", out data);
  }

  public bool LoadFromFile(string filename, out Dictionary<string, string> outData) {
    StreamReader file;
    outData = new Dictionary<string, string>();

    try {
      file = new StreamReader(filename);
    } catch (System.Exception e) {
      Debug.Log("Couldn't load file: " + filename + ", error message: " + e.Message);
      return false;
    }

    data.Clear();

    string content = file.ReadToEnd();
    for (int i = 0; i < content.Length; ++i) {
      if (content[i] == '[') {
        string key = "";
        string value = "";
        int startKeyPosition = i;
        int startValuePosition = 0;

        while (i < content.Length && content[++i] != ']');
        key = content.Substring(startKeyPosition + 1, i - startKeyPosition - 1);

        while (i < content.Length && content[++i] != '{');
        startValuePosition = i;

        while (i < content.Length && content[++i] != '}');
        value = content.Substring(startValuePosition + 1, i - startValuePosition - 1);

        outData[key] = value;
      }
    }

    file.Close();
    return true;
  }
}
