﻿using System.Net.Mime;

namespace TeamsCalendar.Models
{
    public class CalendarEvent
    {
        public CalendarEvent() {
            this.Body = new Body()
            {
                ContentType = "html"
            };
            this.Start = new EventDateTime()
            {
                TimeZone = "Asia/Kolkata"
            };
            this.End = new EventDateTime()
            {
                TimeZone = "Asia/Kolkata"
            };
        } 
        
        public string Subject { get; set; }
        public Body Body { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
        public List<Attendee> Attendees { get; set; }
    }

    public class Body
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class EventDateTime
    {
        public DateTime DateTime { get; set; }
        public string TimeZone { get; set; }
    }

    public class Attendee
    {
        public EmailAddress EmailAddress { get; set; }
        public string type { get; set; }
    }

    public class EmailAddress
    {
        public string address { get; set; }
        public string name { get; set; }
    }
}
