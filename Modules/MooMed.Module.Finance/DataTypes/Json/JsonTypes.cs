using System;
using System.Collections.Generic;
using System.Text;

namespace MooMed.Module.Finance.DataTypes.Json
{
	public class Order
	{
		public string assets { get; set; }
	}

	public class Filter
	{
		public List<string> structure { get; set; }
	}

	public class Meta
	{
		public int page { get; set; }

		public int per_page { get; set; }

		public string sort_direction { get; set; }

		public string sort_by { get; set; }

		public Order order { get; set; }

		public int total_pages { get; set; }

		public int total_records { get; set; }

		public Filter filter { get; set; }
	}

    public class Symbol
    {
	    public string type { get; set; }

	    public string text { get; set; }

	    public string url { get; set; }
    }

    public class Name
    {
	    public string type { get; set; }

	    public string text { get; set; }

	    public string url { get; set; }
    }

    public class OverallRating
    {
	    public string type { get; set; }

	    public string url { get; set; }
    }

    public class Datum
    {
	    public Symbol symbol { get; set; }

	    public Name name { get; set; }

	    public string mobile_title { get; set; }

	    public string price { get; set; }

	    public string assets { get; set; }

	    public string average_volume { get; set; }

	    public string ytd { get; set; }

	    public OverallRating overall_rating { get; set; }

	    public string asset_class { get; set; }
    }
	
	public class RootObject
    {
	    public Meta meta { get; set; }

	    public List<Datum> data { get; set; }
    }
}
