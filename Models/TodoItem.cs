namespace TodoListWebApiWithControllers.Models
{
    public class TodoItem
    {
        // properties
        public long Id {  get; set; }
        public string? Name { get; set; }
        public bool isComplete { get; set; }
        public string? Secret { get; set; }
    }
}
