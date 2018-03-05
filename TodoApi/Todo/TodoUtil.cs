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
    }
}