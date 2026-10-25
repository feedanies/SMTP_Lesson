using System.Net;
using System.Net.Mail;
using System.Xml.Linq;

namespace SMTP_Lesson
{
    internal class Program
    {
        static string fromMail= "fidansdova555@gmail.com";
        static string appPassword= "yflr klbq dbuo vixy";
        static void Main(string[] args)
        {
            Console.WriteLine("==========Registration==========");
            
            string email = "";
            while (true)
            {
                Console.Write("Mail-inizi daxil edin: ");
                email = Console.ReadLine();
                

                try
                {
                    new MailAddress(email);
                    break;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            string password = "";
            while (true)
            {
                Console.Write("Sifre daxil edin (min 6 simvol): ");
                password = Console.ReadLine();
                if (password.Length >= 6)
                {
                    break;
                }
                Console.WriteLine("Sifre en azi 6 simvol olmalidir!");

            }

            var code = new Random().Next(100000, 1000000).ToString();

            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(fromMail);
                mail.To.Add(new MailAddress(email));
                mail.Subject = "Testiq kodu";
                mail.Body = $"Tesdiq kodunuz: {code}\nKod 3 deqiqe etibarlidir.";

                var smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(fromMail, appPassword);
                smtp.Send(mail);
                Console.WriteLine("Kod mail-inize gonderildi. Kod 3 deqiqe etibarlidir.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mail gonderilmedi: " + ex.Message);
                return;
            }

            var time = DateTime.Now.AddMinutes(3);

            for (int i = 1; i < 4; i++)
            {
                Console.Write($"Kodu daxil edin ({i}-ci cehd): ");
                var input = Console.ReadLine();
                if (DateTime.Now > time)
                {
                    Console.WriteLine("Kodun muddeti bitib! Qeydiyyat ugursuz oldu...");
                    return;
                }
                if (input == code)
                {
                    Console.WriteLine($"Qeydiyyat ugurla tamamlandi...");
                    return;
                }
                Console.WriteLine($"Kod yanlisdir! Qalan cehd: {3 - i}");
            }
            Console.WriteLine("Cehd limiti bitdi! Qeydiyyat ugursuz oldu.");
        }
    }
}
