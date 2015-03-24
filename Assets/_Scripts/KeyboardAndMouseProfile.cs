using System;
using System.Collections;
using UnityEngine;
using InControl;


namespace CustomProfile
{
	// custom profile for compatability with mouse and keyboard
	// This custom profile is enabled by adding it to the Custom Profiles list
	// on the InControlManager component, or you can attach it yourself like so:
	// InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
	// 
	public class KeyboardAndMouseProfile : UnityInputDeviceProfile
	{
		public KeyboardAndMouseProfile()
		{
			Name = "Keyboard/Mouse";
			Meta = "A keyboard and mouse combination profile appropriate for FPS.";

			// This profile only works on desktops.
			SupportedPlatforms = new[]
			{
				"Windows",
				"Mac",
				"Linux"
			};

			Sensitivity = 1.0f;
			LowerDeadZone = 0.0f;
			UpperDeadZone = 1.0f;

			ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Load spell - 1",
					Target = InputControlType.Action1,
					Source = KeyCodeButton( KeyCode.Alpha1 )
				},
				new InputControlMapping
				{
					Handle = "Load spell - 2",
					Target = InputControlType.Action2,
					Source = KeyCodeButton( KeyCode.Alpha2 )
				},
				new InputControlMapping
				{
					Handle = "Load spell - 3",
					Target = InputControlType.Action3,
					Source = KeyCodeButton( KeyCode.Alpha3 )
				},
				new InputControlMapping
				{
					Handle = "Load spell - 4",
					Target = InputControlType.Action4,
					Source = KeyCodeButton( KeyCode.Alpha4 )
				},
				new InputControlMapping
				{
					Handle = "Switch F/I",
					Target = InputControlType.LeftBumper,
					// KeyCodeComboButton requires that all KeyCode params are down simultaneously.
					Source = KeyCodeButton( KeyCode.LeftShift )
				},
				new InputControlMapping
				{
					Handle = "Sub Cast",
					Target = InputControlType.RightBumper,
					// KeyCodeComboButton requires that all KeyCode params are down simultaneously.
					Source = MouseButton1 
				},
				new InputControlMapping
				{
					Handle = "Cast Fireball",
					Target = InputControlType.LeftTrigger,
					// KeyCodeComboButton requires that all KeyCode params are down simultaneously.
					Source = MouseButton0
				},
				new InputControlMapping
				{
					Handle = "Run",
					Target = InputControlType.RightTrigger,
					// KeyCodeComboButton requires that all KeyCode params are down simultaneously.
					Source = KeyCodeButton( KeyCode.Space )
				}
			};

			AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Move X",
					Target = InputControlType.LeftStickX,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
				},
				new InputControlMapping
				{
					Handle = "Move Y",
					Target = InputControlType.LeftStickY,
					// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
					Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
				},
				new InputControlMapping {
					Handle = "Move X Alternate",
					Target = InputControlType.LeftStickX,
					Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
				},
				new InputControlMapping {
					Handle = "Move Y Alternate",
					Target = InputControlType.LeftStickY,
					Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
				},
//				new InputControlMapping
//				{
//					Handle = "Look X",
//					Target = InputControlType.RightStickX,
//					Source = m,
//					Raw    = true,
//					Scale  = 0.1f
//				},
//				new InputControlMapping
//				{
//					Handle = "Look Y",
//					Target = InputControlType.RightStickY,
//					Source = MouseYAxis,
//					Raw    = true,
//					Scale  = 0.1f
//				},
				new InputControlMapping
				{
					Handle = "Look Z",
					Target = InputControlType.ScrollWheel,
					Source = MouseScrollWheel,
					Raw    = true,
					Scale  = 0.1f
				}
			};
		}
	}
}

