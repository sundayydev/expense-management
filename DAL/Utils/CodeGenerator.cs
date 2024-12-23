using System;
using NanoidDotNet;

namespace DAL.Utils
{
    public class CodeGenerator
    {
        public string GenerateCodeCategory()
        {
            string uniqueId = Nanoid.Generate(size: 4); // Định nghĩa độ dài mã
            return $"CAT-{uniqueId}";
        }
        
        public string GenerateCodeIncome()
        {
            string uniqueId = Nanoid.Generate(size: 10); // Định nghĩa độ dài mã
            return $"CAT-{uniqueId}";
        }
        
        public string GenerateCodeExpense()
        {
            string uniqueId = Nanoid.Generate(size: 10); // Định nghĩa độ dài mã
            return $"CAT-{uniqueId}";
        }
        
        public string GenerateCodeRecipient()
        {
            string uniqueId = Nanoid.Generate(size: 4); // Định nghĩa độ dài mã
            return $"CAT-{uniqueId}";
        }
        
        public string GenerateCodePayment()
        {
            string uniqueId = Nanoid.Generate(size: 4); // Định nghĩa độ dài mã
            return $"CAT-{uniqueId}";
        }
        
        public string GenerateCodeLoans()
        {
            string uniqueId = Nanoid.Generate(size: 4); // Định nghĩa độ dài mã
            return $"CAT-{uniqueId}";
        }
    }
}