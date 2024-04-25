using TeslaCamPlayer.BlazorHosted.Shared.Models;

namespace TeslaCamPlayer.BlazorHosted.Server.Services.Interfaces;

public interface IClipsService
{
	Task<Clip[]> GetClipsAsync();
	State State { get; }
	void RefreshClips();
}