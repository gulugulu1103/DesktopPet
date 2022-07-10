using DesktopPet.Models;
using System.Collections.Generic;

namespace DesktopPet.Services
{
    interface IPetJsonService
    {
        /// <summary>
        /// 在没有宠物文件时候创建默认宠物文件，常用于Init中
        /// </summary>
        void CreateDefaultPetData();
        /// <summary>
        /// “.\Data\Pets\宠物名”中以Json序列化的方式保存宠物数据
        /// </summary>
        /// <param name="data">要保存的宠物</param>
        void SavePetData(Pet pet);
        /// <summary>
        /// 在“.\dataPath\Pets\宠物名”中以Json序列化的方式保存宠物数据
        /// </summary>
        /// <param name="data">要保存的宠物</param>
        /// <param name="dataPath">文件夹路径</param>
        void SavePetData(Pet pet, string dataPath);
        /// <summary>
        /// 检测“.\Data\Pets”中的宠物列表，校验并返回
        /// </summary>
        /// <returns>检测到的宠物列表</returns>
        List<Pet> GetAllPets();
        /// <summary>
        /// 检测“.\dataPath\Pets”中的宠物列表，校验并返回
        /// </summary>
        /// <param name="dataPath">文件夹名字</param>
        /// <returns>检测到的宠物列表</returns>
        List<Pet> GetAllPets(string dataPath);
    }
}
