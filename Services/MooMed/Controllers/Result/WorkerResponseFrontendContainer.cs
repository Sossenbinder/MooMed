namespace MooMed.Web.Controllers.Result
{
	public class WorkerResponseFrontendContainer
	{
		public string ErrorMessage { get; set; }
	}

    public class WorkerResponseFrontendContainer<TPayload> : WorkerResponseFrontendContainer
    {
        public TPayload Data { get; set; }
    }
}