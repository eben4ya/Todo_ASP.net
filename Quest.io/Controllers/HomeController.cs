using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Quest.io.Models;

namespace Quest.io.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var todoListViewModel = GetAllTodos();
        return View(todoListViewModel);
    }

    internal TodoViewModel GetAllTodos()
    {
        List<Todo> todoList = new();

        using (SqliteConnection con = new SqliteConnection("Data Source = db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM todo";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            todoList.Add(
                                new Todo
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                }
                            );
                        }
                    }
                    else
                    {
                        return new TodoViewModel
                        {
                            todoList = todoList
                        };
                    }
                }
            }
        }
    }
    public RedirectResult Insert(Todo todo)
    {
        using (SqliteConnection con = new SqliteConnection("Data Source = db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"INSERT INTO todo (name) VALUES ('todo.name')";
                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        return Redirect("http://localhost:5054/");
    }

    // We dont use this for now
    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
