using System;
using System.Threading.Tasks;
using Firebase.Database;

namespace Royal.DAO
{
    public class FirebaseCounter
    {
        private FirebaseClient firebaseClient;

        public FirebaseCounter(FirebaseClient client)
        {
            // Sử dụng FirebaseClient được chuyển vào từ bên ngoài
            firebaseClient = client;
        }

        public async Task<int> GetRowCountAsync(string tableName)
        {
            try
            {
                // Đọc dữ liệu từ bảng
                var tableNodes = await firebaseClient.Child(tableName).OnceAsync<object>();

                // Đếm số lượng nút con trong bảng
                int rowCount = 0;
                foreach (var tableNode in tableNodes)
                {
                    rowCount++;
                }

                return rowCount;
            }
            catch (Exception ex)
            {
                // Không hiển thị hộp thoại ở đây vì lớp này không liên quan trực tiếp đến giao diện người dùng
                // Thay vào đó, ném ngoại lệ để cho phép mã gọi xử lý nó
                throw ex;
            }
        }
    }
}
