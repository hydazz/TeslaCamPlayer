using System.Text.Json.Serialization;

namespace TeslaCamPlayer.BlazorHosted.Shared.Models;

public class State
{
	[JsonIgnore]
	public bool IsProcessing => ProcessedFiles < ProcessingFiles;

	[JsonIgnore]
	public int Percentage => (int)((double)ProcessedFiles / ProcessingFiles * 100);

	public string Description { get; set; }

	public int ProcessingFiles { get; set; }

	public int ProcessedFiles { get; set; }

	public State(string description, int processingFiles, int processedFiles)
	{
		Description = description;
		ProcessingFiles = processingFiles;
		ProcessedFiles = processedFiles;
	}

	public State()
	{
	}
}