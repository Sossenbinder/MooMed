namespace MooMed.Web.Controllers.Result
{
	public class ServiceResponseFrontendContainer
	{
		public string ErrorMessage { get; set; }
	}

    public class ServiceResponseFrontendContainer<TPayload> : ServiceResponseFrontendContainer
    {
        public TPayload Data { get; set; }
    }
}