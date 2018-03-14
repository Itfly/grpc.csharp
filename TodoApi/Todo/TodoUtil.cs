namespace Todo
{
    using Proto;

    public static class TodoUtil
    {
        public static TodoItem ToItem(this AddTodoItemRequest request)
        {
            return new TodoItem
            {
                Name = request.Name,
                IsComplete = request.IsComplete
            };
        }

        public static TodoItem ToItem(this UpdateTodoItemRequest request)
        {
            return new TodoItem
            {
                Id = request.Id,
                Name = request.Name,
                IsComplete = request.IsComplete
            };
        }
    }
}