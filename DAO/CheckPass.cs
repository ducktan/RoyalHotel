using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Royal.DAO
{
    public class CheckPass
    {
        public  bool IsStrongPassword(string password)
        {
            // Kiểm tra xem mật khẩu có ít nhất 8 ký tự không
            if (password.Length < 8)
                return false;

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpperCase = true;
                else if (char.IsLower(c))
                    hasLowerCase = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else if (IsSpecialCharacter(c))
                    hasSpecialChar = true;
            }

            // Kiểm tra xem mật khẩu có ít nhất một ký tự in hoa, một ký tự thường, một số và một ký tự đặc biệt không
            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        private bool IsSpecialCharacter(char c)
        {
            // Các ký tự đặc biệt được xác định bởi các ký tự trong khoảng từ ASCII 32 đến 126, ngoại trừ ký tự số và ký tự chữ
            return !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c);
        }
    }
}
