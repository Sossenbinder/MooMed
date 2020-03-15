namespace MooMed.Web.Controllers.Result
{
	public class ServiceResponseFrontendContainer
	{
		public bool Success { get; set; }
	}

    public class ServiceResponseFrontendContainer<TPayload> : ServiceResponseFrontendContainer
    {
        public TPayload Data { get; set; }
    }
}