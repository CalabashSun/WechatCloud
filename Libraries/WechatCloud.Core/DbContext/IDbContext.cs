using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatCloud.Core.DbContext
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Conn { get; }

        void InitConnection();
    }
}
