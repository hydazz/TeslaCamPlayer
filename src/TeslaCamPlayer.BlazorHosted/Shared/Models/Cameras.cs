namespace TeslaCamPlayer.BlazorHosted.Shared.Models;

/// <summary>
/// According to a random post on the internet
/// 0 = front camera
/// 1 = fisheye
/// 2 = narrow
/// 3 = left repeater
/// 4 = right repeater
/// 5 = left B pillar
/// 6 = right B pillar
/// 7 = rear
/// 8 = cabin
/// </summary>
public enum Cameras {
	Unknown = -1,
	Front = 0,
	Fisheye = 1,
	Narrow = 2,
	LeftRepeater = 3,
	RightRepeater = 4,
	LeftBPillar = 5,
	RightBPillar = 6,
	Back = 7,
	Cabin = 8
}