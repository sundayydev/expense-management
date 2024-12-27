using BLL.DTO.Recipient;
using DAL.Models;
using DAL.Repositories;
using DAL.Utils;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class RecipientService
    {
        private readonly RecipientRepository _recipientRepository = new();
        
        public RecipientService()
        { }
        
        public void AddRecipient(AddRecipientDto recipientDto)
        {
            var recipient = new Recipient
            {
                RecipientId = new CodeGenerator().GenerateCodeRecipient(),
                RecipientName = recipientDto.RecipientName,
                UserId = Guid.Parse(recipientDto.UserId),
                Description = recipientDto.Description,
                CreatedAt = DateTime.Now
            };
            
            _recipientRepository.AddRecipient(recipient);
        }

        public void UpdateCategory(Recipient updateRecipient)
        {
            _recipientRepository.UpdateRecipient(updateRecipient);
        }

        public RecipientDto GetRecipientByRecipientId(string recipientId)
        {
            Recipient recipient = _recipientRepository.GetRecipientByRecipientId(recipientId);
            return new RecipientDto()
            {
                RecipientId = recipient.RecipientId,
                RecipientName = recipient.RecipientName,
                Description = recipient.Description,
            };
        }
        
        public List<RecipientDto> GetRecipientsByUserId(string userId)
        {
            List<Recipient> list = _recipientRepository.GetRecipientsByUserId(userId);
            List<RecipientDto> listDto = new();
            
            foreach (var item in list)
            {
                RecipientDto recipientDto = new()
                {
                    RecipientId = item.RecipientId,
                    RecipientName = item.RecipientName,
                    Description = item.Description,
                    CreatedAt = item.CreatedAt.Value.ToString("ss:mm:hh dd/MM/yyyy"),
                };
                
                listDto.Add(recipientDto);
            }
            
            return listDto;
        }

        public void DeleteRecipient(string recipientId)
        {
            Recipient recipient = _recipientRepository.GetRecipientByRecipientId(recipientId);
            _recipientRepository.DeleteRecipient(recipient);
        }
    }
}
