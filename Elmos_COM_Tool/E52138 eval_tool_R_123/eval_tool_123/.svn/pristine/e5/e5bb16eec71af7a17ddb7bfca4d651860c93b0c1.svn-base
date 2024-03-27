# Change Log
### Release 0.10.1
------------------------------------------------------------------------------------------------------------
#### Changes
* Remove shading from color circle

#### Fix
* Sync boost requests

### Release 0.10.0
------------------------------------------------------------------------------------------------------------
#### Features
* Add animation defaults
* Add default setup for patch matrix
* Change colorpicker in LEDAccess view to react on mouse clicks
  anywhere in the color wheel

#### Changes
* Update main view with tools info and new animation group
* Change tool icon to elmos logo

#### Fix
* Fix BUS command transmission for bytes starting with 0x9

### Release 0.9.5
------------------------------------------------------------------------------------------------------------
#### New Feature
* Add BUS command view to log and send write and read messages

#### Changes
* Remove file logging
* Remove remaining GUARD features

### Release 0.9.4
------------------------------------------------------------------------------------------------------------
#### New Feature
* BUS command logging
  Enable in Help menu, created logging file `bus.log`

#### Changes
* Optimize memory consumption of multiple tabs by reuse of one tab page for all chips
* Reduce communication processing
  * Process status updates for current tab only
  * Update PWM, boost and current values only when modified
* Remove vled valid and vdif valid, since it is not supported by validation firmware
* Remove 'ADD GUARD' button

### Release 0.9.3
------------------------------------------------------------------------------------------------------------
#### Fixes
* Apply event handler memory cleanup
* Solve code analyser findings
* Add 4MBaud support for UI
* Enable threading mutex for BUS communication

### Release 0.9.2
------------------------------------------------------------------------------------------------------------
#### Fixes
* Store device addresses in auto addressing temporarily

### Release 0.9.1
------------------------------------------------------------------------------------------------------------
#### New Features
* Added support for Guard M-E.

### Release 0.9
------------------------------------------------------------------------------------------------------------
#### New Features
* Added support for HW revision 2 patch 2.

#### Minor changes
* Added file dialog for CSV export.
* Added warning for HW (in-)compatibility.
* Added mean filter to V17V measurement.
* Voltage measurements displayed with one decimal place.

### Release 0.8
------------------------------------------------------------------------------------------------------------
#### New Features
* Read of valid VLED and VDIF valid flags
* Visualization of valid flags

#### Minor changes
* Added error messages for JTAG_REL_STRG_OL flag

### Release 0.7.1
------------------------------------------------------------------------------------------------------------
#### Minor changes
* Changed board version for R19=62Ohm from v2.0 to v2.1

### Release 0.7
------------------------------------------------------------------------------------------------------------
#### Added Guard Tab Page
* Guard tab page is shown if a Guard firmware variant is detected at address 255.
* A 'General' and a 'Guard' panel is available for a Guard IC.
* Visualization of the guard specific status flags and the corresponding states.
* Visualization of voltage measurements with a status indicator.
* Button for toggeling switch between VBB and VBB_P.
* Button for toggeling TST condition.
* Error log showing the last 5 errors, including an option for saving the last 200 errors in CSV format.

### Release 0.6
------------------------------------------------------------------------------------------------------------
#### New Features
* Add Windows installer to project

#### Minor changes
* Format in Chip Access VLED + VDIF status table is set to two precision digits.
* Multiplexing support in LED Access view

#### Fixes
* Multiplexing status is requested for each chip within Auto Addressing routine
* File link to executable is fixed. A relative path is used now.
* Send API PWM updates in two steps for multiplexing
* Fix boost LEDs and general order of controls in Chip Access view

### Release 0.5
------------------------------------------------------------------------------------------------------------
#### Fixes
* Change table in Auto Addressing menu. All devices shown device address 0x01.
* Multiplexing is now read out from device (requires tapeout version 0.9)

### Release 0.4
------------------------------------------------------------------------------------------------------------
#### Patch Matrix Configuration
The Patch Matrix configuration is loaded into the device for further LED access after the matrix is configured in the device and the _OK_ button is pressed.

#### Minor Changes
* Fix C52138SW-21
  Set a channel than read a channel led to a unusable GUI, which can only released with a restart of the GUI.


### Release 0.3
------------------------------------------------------------------------------------------------------------
#### LED Access
Color picker are added to the LED Access menu. With this control, the user can set up LEDs connected to a 521.38 in RGB, HSV and via a color wheel. To be able to configure the LED, it has to be patched in the Patch Matrix view, accessible via the _PATCH_ button. Each channel of up to 6 LEDs can be assigned to one pin of the chip.

#### Animations
To set the LED values directly, a new command has been added to the animation script language:

* setLED(LED address, RGB=[R, G, B], HSV=[H, S, V])
  Where all values should be within a range of 0..255, aside from H which is in a range of 0..360. See animation examples for more information.

#### Minor changes
* The VLED values has been fixed (VDIF has been displayed)
* Comparator BIST results where erroneously

### Release 0.2
------------------------------------------------------------------------------------------------------------
#### Auto Addressing
In this release, there are two ways to add new devices. Manually device can be add via the 'plus' button next to the device tabs. Additional to that, the Auto Addressing feature is introduced here. The software acts as Auto Addressing Master and allocates addresses to the devices on BUS in an offset of 6 addresses. The first device starts with address 01. The second with 07, and so on. When device addresses exist already in the tool, the user is requested to whether start new instances and discard the settings, or to keep the old instances.

#### Animations
The user can load animations via the animations menu on the main form or in the main menu. The animation file format is in reduced Python language. Possible commands are:
* setPWM(address, [PWM values])
* sleep(seconds)

Also supported are other Python control flow statements from the Python syntax (while, for, ...) and to set up variables. An example is provided in the Animations folder. The animation can be started with the related check box or the Animations menu.

#### Save setups
Also introduced in this release is the possibility to load and save setups in JSON files. The tools saves:
* a device instance for each tab
* and the settings of the instance: PWM, Status, ...

#### Minor changes
* Most commands are now accessible via main menu.
* An Info form is added, to show the copyright and version number.

### Release 0.1
------------------------------------------------------------------------------------------------------------
_INITIAL_
#### GUI Appearance
The initial GUI adapted from the 522.95 project. It differs in the control elements, since not every feature is supported by the 521.38 and additional features has been introduced. Since RGB LED controls will be introduced later, the pure chip access is on another tab, to have insights of the behavior. Generally the GUI can be separated into different areas:

##### Communication Control
In the upper area at top of the main form there are several controls, to set up the communication adapter baud rate, frame style and so on...

##### Device Tab
With a tab control devices can be added to the tool. Via the plus-(+)-button devices can be add manually. On the right side, devices can also be added via auto-addressing. This routine is not implemented yet. Below the tab control, each page contains access to a device.

##### Save + Load
With this control field, configurations for devices can be stored. The functions are not accessible in this release.

##### General Panel
This tab holds information of each chip. Such as about the configured channel, the firmware version and variant, and so on.

##### LED Access Panel
Here, settings for RGB LEDs attached to the device can be configured. The LEDs should be controlled with a HSV picker. The functions on this tab have not been introduced yet.

##### Chip Access Panel
On this tab, the device can be controlled on a chip level. Therefore, the PWM values for each pin is set, not for LEDs attached to the chip. Furthermore, some debug functions are added to check open and short detection and the BIST status. This tab and the functions are introduced in this release.

#### Application Control
The communication with the chip and dimming are part of this release. A timer controlled by the field _Send PWM each ..._ sends the PWM values to the chip and gathers information for the displays on the _Chip Access_ panel.

#### Communication API
The UART communication API provided by Elmos is attached to the Application Controller of the Tool via a communication layer that packs and unpacks messages send to the device. The changes are made in the 521.38 project. Please see the release notes of that project.
