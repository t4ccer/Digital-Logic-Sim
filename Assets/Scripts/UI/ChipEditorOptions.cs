using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChipEditorOptions : MonoBehaviour {

	public enum PinNameDisplayMode {
		AltHover,
		Hover,
		AlwaysMain,
		AlwaysAll
	}

	public class Options
	{
		public PinNameDisplayMode pinNameDisplayMode;
    }

	public Options options;

	public Toggle[] pinDisplayToggles;

	void Awake () {
		//Load saved options
		options = SaveSystem.LoadOptions();
		if (options == null) //Create default options if options file not found
        {
            options = new Options
            {
                pinNameDisplayMode = PinNameDisplayMode.AltHover
            };
            SaveSystem.SaveOptions(options);
        }
		// Toggle correct button
        foreach (var toggle in pinDisplayToggles)
			toggle.isOn = false;
		pinDisplayToggles[(int)options.pinNameDisplayMode].isOn = true;

        pinDisplayToggles[0].onValueChanged.AddListener ((isOn) => SetPinNameMode (isOn, PinNameDisplayMode.AltHover));
		pinDisplayToggles[1].onValueChanged.AddListener ((isOn) => SetPinNameMode (isOn, PinNameDisplayMode.Hover));
		pinDisplayToggles[2].onValueChanged.AddListener ((isOn) => SetPinNameMode (isOn, PinNameDisplayMode.AlwaysMain));
		pinDisplayToggles[3].onValueChanged.AddListener ((isOn) => SetPinNameMode (isOn, PinNameDisplayMode.AlwaysAll));
	}

	void SetPinNameMode (bool isOn, PinNameDisplayMode mode) {
		if (isOn) {
			options.pinNameDisplayMode = mode;
			SaveSystem.SaveOptions(options);
		}
	}

}