using PayPalDemo.Models;

namespace PayPalDemo.Repositories
{
    public class MyRegisteredUserRepo
    {
        private readonly ApplicationDbContext _context;

        public MyRegisteredUserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddNewUser(MyRegisteredUser user)
        {
            _context.MyRegisteredUsers.Add(user);
            _context.SaveChanges();
        }



        public string GetUserNameByEmail(string userEmail)
        {
            var user = _context.MyRegisteredUsers.FirstOrDefault(u => u.Email.ToLower() == userEmail.ToLower());
            return user.FirstName + " " + user.LastName;
         

        }

        
    }
}
