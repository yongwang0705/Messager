using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace MessagerServer
{
    class DataBusiness
    {
        public bool VerifierUser(string id,string passwd,ref string userName)
        {
            string sqlstr = "select name from userlist where id='"+ id + "' and passwd='"+passwd+"'";
            userName =MDatabaseOperator.QueryFirstOperation(sqlstr);
            return userName != null;
            //if (userName!=null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public Int32 CreateUser(string userName,string passwd)
        {
            byte[] randomBytes = new byte[8];
            int id=0;
            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();

            rngCrypto.GetBytes(randomBytes);
            id = BitConverter.ToInt32(randomBytes, 0);
            if (id < 0)
                id = id * (-1);
            //id= new Random(Guid.NewGuid().GetHashCode()).Next(1, 10000000); 
            string sqlstr = "insert into userlist values(" + id + ",'" + userName + "','" + passwd + "')";
            //if (MDatabaseOperator.ExecuteOperation(sqlstr) > 0)
            //{
            //    return id;
            //}
            //else
            //{
            //    return 0;
            //}
            return MDatabaseOperator.ExecuteOperation(sqlstr) > 0 ? id : 0;
        }
    }
}
