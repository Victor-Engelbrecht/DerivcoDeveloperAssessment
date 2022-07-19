using System;

namespace Models
{
	public class ErrorResponse
	{
		public String Type { get; set; }
		public String Message { get; set; }
		public String StackTrace { get; set; }
		public ErrorResponse(Exception ex)
		{
			Type = ex.GetType().Name;
			Message = ex.Message;
			StackTrace = ex.ToString();
		}
	}
}
