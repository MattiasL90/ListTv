using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListTv.ViewModels
{
    public class ProgramVM
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string ProgramName { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.TimeSpan> Length { get; set; }
        public string Info { get; set; }


    }
}