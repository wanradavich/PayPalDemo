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
            var user = _context.MyRegisteredUsers.FirstOrDefault(u => u.Email == userEmail);
            return user.FirstName + " " + user.LastName;

        }

        //public void GetUserNameByEmail(string email, string firstName, string lastName)
        //{
        //    MyRegisteredUser registerUser = new MyRegisteredUser()
        //    {
        //        Email = email,
        //        FirstName = firstName,
        //        LastName = lastName
        //    };
        //    _context.MyRegisteredUsers.Add(registerUser);
        //    _context.SaveChanges();
        //}
    }
}
