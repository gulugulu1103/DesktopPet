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
        /// 调用T的无参构造器创建一个桌宠样例实例并返回
        /// </summary>
        /// <typeparam name="T">Pet或者其子类</typeparam>
        /// <returns></returns>
        Models.Pet CreateSamplePet<T>();
    }
}
