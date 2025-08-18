using NextFlix.Domain.Concretes;
using NextFlix.Domain.Enums;

namespace NextFlix.Domain.Entities
{
	public class Log:EntityBase
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public LogType LogType { get; set; }
		public string TableName { get; set; }
		public DateTime LogDate { get; set; } = DateTime.UtcNow;
		public int? UserId { get; set; }
		public virtual User? User { get; set; }
	}
}
