using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Pic { get; set; }
        public DateTime MessageSendTime { get; set; }
        public string FormattedMessageSendTime { get; set; } // Add this property
    }

}
