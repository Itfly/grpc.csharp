﻿namespace TodoServer
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using Todo.Proto;

    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) 
            : base(options)
        {
            Init();
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public Guid Id { get; set; }

        private void Init()
        {
            TodoItems.Add(new TodoItem { Name = "Item1" });
            this.SaveChanges();
            Id = Guid.NewGuid();
        }
    }
}