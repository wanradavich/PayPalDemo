using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayPalDemo.ViewModels;

namespace PayPalDemo.Repositories
{
    public class RoleRepo
    {
        private readonly ApplicationDbContext _db;

        public RoleRepo(ApplicationDbContext context)
        {
            this._db = context;
            CreateInitialRole();
        }

        public IEnumerable<RoleVM> GetAllRoles()
        {
            var roles =
                _db.Roles.Select(r => new RoleVM
                {
                    RoleName = r.Name,
                    Id = GenerateRoleId(r.Name)
                });

            return roles;
        }

        public RoleVM GetRole(string roleName)
        {


            var role = _db.Roles.Where(r => r.Name == roleName)
                                .FirstOrDefault();

            if (role != null)
            {
                return new RoleVM() { RoleName = role.Name };
            }
            return null;
        }

        public bool CreateRole(string roleName, out RoleVM createdRole)
        {
            createdRole = null;

            try
            {
              
                IdentityRole newRole = new IdentityRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                    
                };

                _db.Roles.Add(newRole);
                _db.SaveChanges();

                string roleId = newRole.Id;

                createdRole = new RoleVM
                {
                    RoleName = roleName,
                    Id = roleId
                };

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public SelectList GetRoleSelectList()
        {
            var roles = GetAllRoles().Select(r => new
            SelectListItem
            {
                Value = r.RoleName,
                Text = r.RoleName
            });

            var roleSelectList = new SelectList(roles,
                                               "Value",
                                               "Text");
            return roleSelectList;
        }

        public void CreateInitialRole()
        {
            const string ADMIN = "Admin";

            var role = GetRole(ADMIN);

            if (role == null)
            {
                RoleRepo roleRepo = new RoleRepo(_db);
                CreateRole(ADMIN, out RoleVM createdRole);
            }
        }
        // Logic for role deletion can be included here.

        public bool DeleteRole(string roleName)
        {
            bool isSuccess = true;
            bool roleCount = _db.UserRoles.Any(ur => ur.RoleId == roleName);
            if (roleCount)
            {
                isSuccess = false;
            }
            else
            {
                try
                {
                    var role = _db.Roles
                              .Where(r => r.Name == roleName)
                              .FirstOrDefault();
                    if (role != null)
                    {
                        _db.Roles.Remove(role);
                        _db.SaveChanges();
                    }
                    else
                    {
                        isSuccess = false; // Role not found
                    }
                }
                catch (Exception)
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }

        public static string GenerateRoleId(string roleName)
        {
            if (string.IsNullOrEmpty(roleName) || roleName.Length < 2)
            {
                throw new ArgumentException("Role Name should have at least 2 letters");
            }

            string id = roleName.Substring(0, 2);
            return id;
        }

    }

}
