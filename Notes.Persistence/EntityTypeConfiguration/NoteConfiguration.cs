﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain;

namespace Notes.Persistence.EntityTypeConfiguration
{
    // класс NoteConfiguration создан для вынесения конфигураций каких-то 
    // полей(может и не только полей) и интерфейс IEntityTypeConfiguration
    // реализует это что бы облегчить читаймость метода OnModelCreating
    // находящийся в классе NotesDbContext 

    internal class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(note => note.Id);
            builder.HasIndex(note => note.Id).IsUnique();
            builder.Property(note => note.Title).HasMaxLength(250);

        }
    }
}
