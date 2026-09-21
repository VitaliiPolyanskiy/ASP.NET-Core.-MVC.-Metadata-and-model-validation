using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Validation.StudentsDb.Annotations;

public class MyAuthorsAttribute(string[] authors) : ValidationAttribute
{
    private readonly string[] _authors = authors;

    public override bool IsValid(object? value)
    {
        if (value is string strVal)
        {
            return _authors.Contains(strVal);
        }
        return false;
    }
}