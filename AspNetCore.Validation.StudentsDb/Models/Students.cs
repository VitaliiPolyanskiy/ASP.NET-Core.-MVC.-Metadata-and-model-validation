using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Validation.StudentsDb.Models;

// Для взаємодії з MS SQL Server через Entity Framework необхідний пакет 
// Microsoft.EntityFrameworkCore.SqlServer

public class StudentContext: DbContext
{
    // Кожна властивість DbSet співвідноситиметься з окремою таблицею в базі даних.
    public DbSet<Student> Students { get; set; }
    public DbSet<Book> Books { get; set; }

    public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }
}

public class Student
{
    // Ідентифікатор студента
    public int Id { get; set; }

    // Ім'я студента
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [Display(Name = "Ім'я студента")]
    public required string Name { get; set; }

    // Прізвище студента
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [Display(Name = "Прізвище студента")]
    public required string Surname { get; set; }

    // Вік студента
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [Display(Name = "Вік")]
    [Range(15, 60, ErrorMessage = "Неприпустимий вік.")]
    public int Age { get; set; }

    // Середній бал
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [Range(0.0, 12.0, ErrorMessage = "Неприпустимий середній бал.")]
    [Display(Name = "Середній бал")]
    public double GPA { get; set; }

    // Електронна пошта
    [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Некоректна адреса.")]
    [Remote(action: "CheckEmail", controller: "Students", ErrorMessage = "Email вже використовується.")]
    [Display(Name = "Електронна пошта")]
    public required string Email { get; set; }

    /*
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage="Паролі не збігаються")]
    [DataType(DataType.Password)]
    public string PasswordConfirm { get; set; }

    Currency відображає текст у вигляді валюти
    DateTime відображає дату і час
    Date відображає тільки дату, без часу
    Time відображає тільки час
    Text відображає однорядковий текст
    MultilineText відображає багаторядковий текст (елемент textarea)
    Password відображає символи з використанням маски
    Url відображає рядок URL
    EmailAddress відображає електронну адресу
     */
}