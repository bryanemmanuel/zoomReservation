using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoomrs.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required]
        public DateOnly date { get; set; }
        [Required]
        public int time { get; set; }
        //public TimeOnly starttime { get; set; }
        //public TimeOnly endtime { get; set; }
        [Required]
        public int count { get; set; }
        public string availability { get; set; }

        public int? AppointmentId { get; set; }

    }
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
        { }
    }
    //public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    //{
    //    public TimeOnlyConverter() : base(
    //        timeOnly => timeOnly.ToTimeSpan(),
    //        timeSpan => TimeOnly.FromTimeSpan(timeSpan))
    //    { }
    //}
}
