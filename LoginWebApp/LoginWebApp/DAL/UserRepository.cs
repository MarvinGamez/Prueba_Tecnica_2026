using System.Data;
using Microsoft.Data.SqlClient;
using LoginWebApp.Models;

namespace LoginWebApp.DAL
{
    public class UserRepository
    {
        private readonly string _connection;

        public UserRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("UsersCrudConnection");
        }

        public User Login(string username, string passwordHash)
        {
            using SqlConnection cn = new SqlConnection(_connection);
            using SqlCommand cmd = new SqlCommand("spUsers_Login", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.Char, 64).Value = passwordHash;

            cn.Open();

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return new User
                {
                    UserId = Convert.ToInt32(dr["UserId"]),
                    FullName = dr["FullName"].ToString(),
                    Role = Convert.ToInt32(dr["Role"]),
                    IsActive = Convert.ToBoolean(dr["IsActive"])
                };
            }

            return null;
        }


        public void Register(User user, string passwordHash)
        {
            using SqlConnection cn = new SqlConnection(_connection);
            using SqlCommand cmd = new SqlCommand("spUsers_Insert", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = user.FullName;
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = user.Username;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 120).Value = user.Email;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.Char, 64).Value = passwordHash;
            cmd.Parameters.Add("@Role", SqlDbType.Int).Value = user.Role;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = true;

            cn.Open();
            cmd.ExecuteNonQuery();
        }


    }
}
