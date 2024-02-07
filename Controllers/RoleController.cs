using Microsoft.AspNetCore.Mvc;
using PayPalDemo.Repositories;
using PayPalDemo.ViewModels;
using System.Data;

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

        //[HttpPost]
        //public IActionResult Create(RoleVM roleVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            RoleRepo roleRepo = new RoleRepo(_context);

        //            // Set Id manually based on the first two letters of RoleName
        //            roleVM.Id = RoleRepo.GenerateRoleId(roleVM.RoleName);

        //            bool isSuccess = roleRepo.CreateRole(roleVM.RoleName, out RoleVM createdRole);

        //            if (isSuccess)
        //            {
        //                // Optionally, set a success message in ViewBag
        //                ViewBag.Message = "Role created successfully.";

        //                return RedirectToAction(nameof(Index));
        //            }
        //            else
        //            {
        //                // Handle the case where role creation fails
        //                ViewBag.ErrorMessage = "Failed to create the role.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exceptions, log, or display an error message
        //            ViewBag.ErrorMessage = $"Error creating role: {ex.Message}";
        //        }
        //    }

        //    // If ModelState is not valid or role creation fails, return to the Create view with the provided RoleVM
        //    return View(roleVM);
        //}


        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {

            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_context);
                bool isSuccess =
                    roleRepo.CreateRole(roleVM.RoleName, out RoleVM createdRole);

                if (isSuccess)
                {

                    string message = "Role created successfully";
                    return RedirectToAction("Index", "Role",
                        new
                        {
                            id = createdRole.Id,
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
