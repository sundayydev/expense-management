using DAL.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL.Repositories
{
    public class RecipientRepository
    {
        private readonly MyDBContext _context = new MyDBContext();
        
        public RecipientRepository()
        { }
        
        public void AddRecipient(Recipient recipient)
        {
            _context.Recipients.Add(recipient);
            _context.SaveChanges();
        }
        
        public List<Recipient> GetRecipients()
        {
            return _context.Recipients.ToList();
        }

        public void UpdateRecipient(Recipient recipient)
        {
            var recipientUpdate = _context.Recipients.FirstOrDefault(r => r.RecipientId == recipient.RecipientId);

            if (recipientUpdate != null)
            {
                recipientUpdate.RecipientName = recipient.RecipientName;
                recipientUpdate.Description = recipient.Description;
            }
         
            _context.Recipients.AddOrUpdate(recipientUpdate);
            _context.SaveChanges();
        }
        
        public Recipient GetRecipientByRecipientId(string recipientId)
        {
            return _context.Recipients.FirstOrDefault(r => r.RecipientId == recipientId);
        }

        public List<Recipient> GetRecipientsByUserId(string userId)
        {
            return _context.Recipients
                .AsNoTracking() // Không theo dõi các thực thể
                .Where(c => c.UserId.ToString() == userId || c.UserId == null)
                .ToList();
        }

        public void DeleteRecipient(Recipient recipient)
        {
            _context.Recipients.Remove(recipient);
            _context.SaveChanges();
        }
    }
}
