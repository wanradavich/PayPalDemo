using Microsoft.AspNetCore.Mvc;
using PayPalDemo.Repositories;
using PayPalDemo.ViewModels;

namespace PayPalDemo.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index(string message = "")
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            ViewBag.Message = message;
            return View(roleRepo.GetAllRoles());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_context);
                bool isSuccess =
                    roleRepo.CreateRole(roleVM.RoleName);

                if (isSuccess)
                {
                    string message = "Role created successfully";
                    return RedirectToAction("Index", "Role",
                        new
                        {
                            message = message
                        });

                }
                else
                {
                    ModelState
                    .AddModelError("", "Role creation failed.");
                    ModelState
                    .AddModelError("", "The role may already" +
                                       " exist.");
                }
            }

            return View(roleVM);
        }

        public ActionResult Delete(string roleName)
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            var role = roleRepo.GetRole(roleName);
            return View(role);
        }
        [HttpPost]
        public ActionResult Delete(RoleVM RoleVM)
        {

            RoleRepo roleRepo = new RoleRepo(_context);
            var role = roleRepo.GetRole(RoleVM.RoleName);

            bool isSuccess = roleRepo.DeleteRole(role.RoleName);
            string message = "";
            if (isSuccess)
            {
                message = $"'{role.RoleName}' removed successfully.";
                return RedirectToAction("Index", "Role",
                  new
                  {
                      message = message
                  });
                //string message = "Success";
                //return RedirectToAction("Index", "Role", new { message = message });
            }
            else
            {
                ModelState.AddModelError("", "Role deletion failed.");
                return View("Delete", role); // Return to the delete confirmation view with error message
            }
        }

    }
}
