using System;
using System.Collections;
using System.Collections.Generic;


namespace InControl
{
	[AutoDiscover]
	public class KeyboardProfile : UnityInputDeviceProfile
	{
		public KeyboardProfile()
		{
			Name = "Keyboard";
			Meta = "";

			SupportedPlatforms = new[]
			{
				"Windows",
				"Mac",
				"Linux"
			};

			Sensitivity = 1.0f;
			DeadZone = 0.0f;

			ButtonMappings = new[]
			{
				new InputControlButtonMapping()
				{
					Handle = "W Key",
					Target = InputControlType.Action4,
					Source = "w"
				},
				new InputControlButtonMapping()
				{
					Handle = "A Key",
					Target = InputControlType.Action3,
					Source = "a"
				},
				new InputControlButtonMapping()
				{
					Handle = "S Key",
					Target = InputControlType.Action1,
					Source = "s"
				},
				new InputControlButtonMapping()
				{
					Handle = "D Key",
					Target = InputControlType.Action2,
					Source = "d"
				},
				new InputControlButtonMapping() {
					Handle = "Q Key",
					Target = InputControlType.LeftBumper,
					Source = "q"
				},
				new InputControlButtonMapping() {
					Handle = "E Key",
					Target = InputControlType.RightBumper,
					Source = "e"
				},
				new InputControlButtonMapping()
				{
					Handle = "Button 1 Trigger",
					Target = InputControlType.LeftTrigger,
					Source = "1"
				},
				new InputControlButtonMapping()
				{
					Handle = "Button 2 Trigger",
					Target = InputControlType.RightTrigger,
					Source = "2"
				}
			};

			AnalogMappings = new InputControlAnalogMapping[]
			{
				new InputControlAnalogMapping()
				{
					Handle = "Arrow Keys X",
					Target = InputControlType.LeftStickX,
					Source = "left right"
				},
				new InputControlAnalogMapping()
				{
					Handle = "Arrow Keys Y",
					Target = InputControlType.LeftStickY,
					Source = "down up"
				}
			};
		}
	}
}

