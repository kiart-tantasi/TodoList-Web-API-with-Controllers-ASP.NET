namespace TodoListWebApiWithControllers.Models
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool isComplete { get; set; }
    }
}
