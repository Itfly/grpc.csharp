namespace Todo
{
    using Proto;

    public static class TodoUtil
    {
        public static TodoItem NullTodoItem = new TodoItem
        {
            Name = string.Empty 
        };

        public static bool Exists(this TodoItem item)
        {
            return item != null && (item.Name.Length != 0);
        }

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