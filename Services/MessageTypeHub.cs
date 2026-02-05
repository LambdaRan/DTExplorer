namespace DTExplorer.Services;

// 底部状态信息
public sealed class StatusMessage
{
	public string Status { get; set; }
	public bool IsError { get; set; }
	public bool IsToLog { get; set; }
	public StatusMessage(string status, bool isError, bool log)
	{
		Status = status;
		IsError = isError;
		IsToLog = log;
	}
	public static StatusMessage Empty => new StatusMessage(string.Empty, false, false);
	public static StatusMessage Normal(string message) => new StatusMessage(message, false, false);
	public static StatusMessage Error(string message) => new StatusMessage(message, true, false);
	public static StatusMessage NormalWithLog(string message) => new StatusMessage(message, false, true);
	public static StatusMessage ErrorWithLog(string message) => new StatusMessage(message, true, true);
}

