using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Services
{
    interface ISettingsJsonService
    {
        /// <summary>
        /// 在没有设定文件时候创建默认设定文件，常用于Init中
        /// </summary>
        void CreateDefaultSettings();
        /// <summary>
        /// 从“\Saves\Settings.json”读取设定到SettingsHolder.settings中
        /// </summary>
        void GetSettings();
        /// <summary>
        /// 从“\savePath\Settings.json”读取设定到SettingsHolder.settings中
        /// </summary>
        /// <param name="savePath">文件夹名称</param>
        void GetSettings(string savePath);
        /// <summary>
        /// 从“\savePath\Settings.json”读取设定到settings中
        /// </summary>
        /// <param name="settings">输出的settings</param>
        /// <param name="savePath">文件夹名称</param>
        void GetSettings(ref DesktopPetSettings settings, string savePath);
        /// <summary>
        /// 序列化SettingsHolder.settings到“.\Saves\Settings.json”中
        /// </summary>
        void SaveSettings();
        /// <summary>
        /// 序列化settings到“.\Saves\Settings.json”中
        /// </summary>
        /// <param name="settings">要保存的DesktopPetSettings</param>
        void SaveSettings(DesktopPetSettings settings);
        /// <summary>
        /// 序列化settings到“.\savePath\Settings.json”中
        /// </summary>
        /// <param name="settings">要保存的DesktopPetSettings</param>
        /// <param name="savePath">保存的文件夹名称</param>
        void SaveSettings(DesktopPetSettings settings, string savePath);
    }
}
