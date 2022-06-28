namespace EmailWorkerService.Service
{
    public class MailService : IMailService
    {
        private readonly AppDbContext _context;
        public MailService(AppDbContext context)
        {
            _context = context;
        }


        public void SendEmail()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                File.AppendAllText(@"C:\hangfire.txt", $"Kullanıcı Adı: {user.UserName} Email: {user.Email}\n");
            }
            Console.WriteLine("Email gönderildi.");
        }
    }
}
