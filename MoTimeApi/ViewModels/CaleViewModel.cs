using System.ComponentModel.DataAnnotations;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;

namespace MoTimeApi.ViewModels
{
    public class CaleViewModel
    {
        public string CalendarItemName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
