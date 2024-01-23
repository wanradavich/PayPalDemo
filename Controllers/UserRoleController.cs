using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPalDemo.Repositories;
using PayPalDemo.ViewModels;

namespace PayPalDemo.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleController(ApplicationDbContext context,
                                 UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Index(string message = "")
        {
            UserRepo userRepo = new UserRepo(_context);
            IEnumerable<UserVM> users = userRepo.GetAllUsers();

            ViewBag.Message = message;
            return View(users);
        }

        public async Task<IActionResult> Detail(string userName,
                                                string message = "")
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);
            MyRegisteredUserRepo myRegisteredUserRepo = new MyRegisteredUserRepo(_context);
            var registeredUser = myRegisteredUserRepo.GetUserNameByEmail(userName);



            var roles = await userRoleRepo.GetUserRolesAsync(userName);

            ViewBag.Message = message;
            ViewBag.UserName = registeredUser;

            return View(roles);
        }

        public ActionResult Create()
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            ViewBag.RoleSelectList = roleRepo.GetRoleSelectList();


            UserRepo userRepo = new UserRepo(_context);
            ViewBag.UserSelectList = userRepo.GetUserSelectList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);

            if (ModelState.IsValid)
            {
                try
                {
                    var addUR =
                    await userRoleRepo.AddUserRoleAsync(userRoleVM.Email,
                                                        userRoleVM.RoleName);

                    string message = $"{userRoleVM.RoleName} permissions" +
                                     $" successfully added to " +
                                     $"{userRoleVM.Email}.";

                    return RedirectToAction("Detail", "UserRole",
                                      new
                                      {
                                          userName = userRoleVM.Email,
                                          message = message
                                      });
                }
                catch
                {
                    ModelState.AddModelError("", "UserRole creation failed.");
                    ModelState.AddModelError("", "The Role may exist " +
                                                 "for this user.");
                }
            }

            RoleRepo roleRepo = new RoleRepo(_context);
            ViewBag.RoleSelectList = roleRepo.GetRoleSelectList();

            UserRepo userRepo = new UserRepo(_context);
            ViewBag.UserSelectList = userRepo.GetUserSelectList();

            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Delete(string role, string email)
        {
            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(email))
            {
                return BadRequest(); // Handle invalid input
            }


            return View(new UserRoleVM
            {
                Email = email,
                RoleName = role
            }); // Return the UserRole details for confirmation before my delete
        }



        [HttpPost]
        public async Task<IActionResult> Delete(UserRoleVM userRole)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_userManager);
            // Perform the deletion of UserRole
            var isSuccess = await userRoleRepo.RemoveUserRoleAsync(userRole.Email, userRole.RoleName);
            string message = "";
            if (isSuccess)
            {
                message = $"'{userRole.RoleName}' removed successfully for user '{userRole.Email}'.";
                return RedirectToAction("Detail", "UserRole",
                  new
                  {
                      message = message
                  });

            }
            else
            {
                message = $"Failed to remove '{userRole.RoleName}' for  '{userRole.Email}'.";
            }
            // Redirect to the Detail page with the appropriate message
            return RedirectToAction(nameof(Detail), new { userName = userRole.Email });
        }



    }
}
