using System;
using System.Net;

namespace Models
{
	public class ErrorException : Exception
	{
		public HttpStatusCode StatusCode { get; private set; }
		public ErrorException(HttpStatusCode status, String msg, Object data = null) : base(msg)
		{
			StatusCode = status;
		}
	}
}
