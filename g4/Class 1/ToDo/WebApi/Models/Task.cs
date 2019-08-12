using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Models
{
    public class Task
    {
        [BindNever]
        public int Id { get; set; }

        public string Title { get; set; }

    }
}
