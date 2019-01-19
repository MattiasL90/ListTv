using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListTv.ViewModels
{
    public class ShowMore
    {

        public int Id { get; set; }
        public string ProgramName { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Category { get; set; }
        public Nullable<System.TimeSpan> Length { get; set; }
        public string ChannelName { get; set; }
        public string Info { get; set; }
    }
}