using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Services
{
    interface IInitService
    {
        /// <summary>
        /// 在安装初次运行时，软件将创建用户环境：创建默认设定和默认宠物
        /// </summary>
        void FirstInit();
        /// <summary>
        /// 每次软件启动时，检测文件是否到位，不到位则新建一个。
        /// </summary>
        void EnvironmentCheck();
        /// <summary>
        /// 创建一个简易桌宠的样例宠物并返回
        /// </summary>
        /// <returns>一个简易桌宠的样例宠物</returns>
        Models.Pet CreateSamplePet();
    }
}
