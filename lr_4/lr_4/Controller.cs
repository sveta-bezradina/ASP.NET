using Microsoft.AspNetCore.Mvc;

namespace lr_4
{
    [ApiController]
    [Route("Library")]
    public class BookController : ControllerBase
    {
        private readonly IConfiguration _config;

        public BookController(IConfiguration config)
        {
            this._config = config;
        }

        [HttpGet]
        public IActionResult GetGreeting()
        {
            return Ok("Hello user, welcome to the library!");
        }

        [HttpGet("Book")]
        public IActionResult GetAllBooks()
        {
            var books = _config.GetSection("Books").Get<List<Book>>();

            if (books.Count == 0 || books == null)
            {
                return BadRequest("No books");
            }
            var booksStr = string.Join("\n", books.Select(book => book.ToString()));

            return Ok(booksStr);
            
        }
        [HttpGet("Profile/{id:int?}")]
        public IActionResult GetProfile(int? id) {

            if (id == null) 
            {
                var currentUser = _config.GetSection("CurrentUser").Get<User>();
                if (currentUser == null)
                {
                    return BadRequest("Current user not found");
                }
                return Ok(currentUser.ToString());

            }

            if (id < 0 || id > 5) return NotFound("Must be an integer between 0 and 5");

            var users = _config.GetSection("Users").Get<List<User>>();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return BadRequest($"User with {id} id not found");
            }
            return Ok(user.ToString());
        }
    }

    public class Book
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Price}";
        }
    }

    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Age}\n";
        }
    }
}
