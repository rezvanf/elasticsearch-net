﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The response from deleting a calendar job.
	/// </summary>
	public class DeleteCalendarJobResponse : ResponseBase
	{
		[DataMember(Name = "calendar_id")]
		public string CalendarId { get; internal set; }

		[DataMember(Name = "description")]
		public string Description { get; internal set; }

		[DataMember(Name = "job_ids")]
		public IReadOnlyCollection<Id> JobIds { get; internal set; } = EmptyReadOnly<Id>.Collection;
	}
}
