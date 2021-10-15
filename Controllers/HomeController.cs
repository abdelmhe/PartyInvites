using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Linq;


namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View("SignUp");
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }

        }

        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(r => r.WillAttend == true));
        }

        [HttpGet]
        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ViewResult SignUp(UserSignUp request)
        {
            if (ModelState.IsValid)
            {
                UserInfo temp = new UserInfo();
                temp.FirstName = request.FirstName;
                temp.LastName = request.LastName;
                temp.Email = request.Email;
                temp.Phone = request.Phone;
                temp.Password = request.Password;

                Repository.AddUser(temp);

                return View("Login");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Login(UserInfo request)
        {
            if (ModelState.IsValid)
            {
                var user = Repository.Users.Where(user => user.Email == request.Email).FirstOrDefault();
                if (user != null && user.Password == request.Password)
                {
                    Repository.LogInUser(user);

                    return View("Dashboard", Repository.LoggedInUser);
                }
                else
                {
                    return View(request);
                }
            }
            else
            {
                return View(request);
            }

        }

        public ViewResult SignOut()
        {
            Repository.LogOutUser();
            return View("Login");
        }

        public ViewResult Dashboard()
        {
            if (Repository.LoggedInUser != null && Repository.LoggedInUser.Email != null)
            {
                return View("Dashboard", Repository.LoggedInUser);
            }
            else
            {
                return View("Login");
            }
        }

        public ViewResult ListUsers()
        {
            if (Repository.LoggedInUser != null && Repository.LoggedInUser.Email != null)
            {
                return View("ListUsers", Repository.Users);
            }
            else
            {
                return View("Login");
            }
        }
    }
}
