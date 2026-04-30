using System.ComponentModel.DataAnnotations;

public class EmployeeRequestDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, RegularExpression(@"^\d{10}$")]
    public string Phone { get; set; }

    [Required] public string Department { get; set; }
    [Required] public string Designation { get; set; }

    [Range(1, double.MaxValue)]
    public decimal Salary { get; set; }

    public DateTime JoinDate { get; set; }

    [Required] public string Status { get; set; }
}

public class EmployeeResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Department { get; set; } = "";
    public string Designation { get; set; } = "";
    public decimal Salary { get; set; }
    public DateTime JoinDate { get; set; }
    public string Status { get; set; } = "";
}

public class PagedResult<T>
{
    public List<T> Data { get; set; } = new();

    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool HasNextPage => Page < TotalPages;
    public bool HasPrevPage => Page > 1;
}


public class EmployeeQueryParams
{
    public string? Search { get; set; }
    public string? Department { get; set; }
    public string? Status { get; set; }
    public string? SortBy { get; set; } = "name";
    public string? SortDir { get; set; } = "asc";

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
