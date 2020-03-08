using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayUIUpload.Controllers
{
    public class UploadImageController : Controller
    {
        // GET: UploadImage
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public string UploadFile()
        {
            var file = Request.Files[0];
            int BUFFERSIZE = 4096;
            //设定容器最大值
            var name = string.Empty;
            //    var uploadTime = DateTime.Now;
            ///文件上传的最底层目录路径 格式为 \文件id\文件名
            var fileId = Guid.NewGuid();
            name = file.FileName;
            //   var uploadPath = Path.Combine(fileId.ToString(), name);
            ///文件上传后的位置
            var outputPath = Path.Combine(@"C:\Users\62619\Desktop\layui上传缓存", name);
            ///将接收到的文件转化为流
            var inputStream = file.InputStream;
            ///流数据读取到数组中的偏移量
            long offset = 0;
            ///获取或设置光标在当前流中的位置
            inputStream.Position = offset;
            ///存储流中数据的数组
            //    byte[] buffer = new byte[BUFFERSIZE];
            ///读取流中的数据,读到数组中
            while (offset < inputStream.Length)
            {
                ///存储流中数据的数组,该数组大小根据流中未读取数据量大小调整，若未读取数据量大于规定的数组最大大小，则数组大小设为该数组的最大容量
                byte[] buffer = new byte[Math.Min(BUFFERSIZE, inputStream.Length - offset)];
                int nRead = inputStream.Read(buffer, 0, buffer.Length);
                if (nRead < 0)
                {
                    ///若读取完毕，则跳出循环
                    break;
                }
                try
                {
                    ///将读取到数组中的数据写入新的文件中，再保存到指定的位置
                    using (var outputStream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        outputStream.Seek(offset, SeekOrigin.Begin);//将流中的光标移到第一次读取的数据之后
                        outputStream.Write(buffer, 0, buffer.Length);
                        outputStream.Flush();

                        offset = outputStream.Length;
                    }
                }
                catch (Exception exception)
                {

                    throw exception;
                }

            }
            return JsonConvert.SerializeObject(new { Id = fileId, Name = name });
        }


        public string Upload()
        {
            ///获取上传的文件
            var file = Request.Files[0];
            //获取上传文件的文件名
            string fileName = file.FileName;
            //上传路径
            string filePath = Path.Combine(@"C:\Users\62619\Desktop\layui上传缓存", fileName);
            //定义缓存数组
            byte[] buffer;
            //将文件数据塞到流里
            var inputStream = file.InputStream;
            ///获取读取数据的长度
            int readLength = Convert.ToInt32(inputStream.Length);
            ///给缓存数组指定大小
            buffer = new byte[readLength];
            //设置指针的位置为 最开始 的位置
            inputStream.Seek(0, SeekOrigin.Begin);
            //从位置 0 开始读取上传的文件的数据，数据读取到第一个参数buffer(缓存区)中
            inputStream.Read(buffer, 0, readLength);
            //创建输出文件流,指定文件的输出位置,模式为创建该新文件,读写权限为 写
            using (var outputStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                //设置指针的位置为 最开始 的位置
                outputStream.Seek(0, SeekOrigin.Begin);
                //从起始位置 将 第一个参数 buffer（缓存区）里的数据写入到 filePath 指定的文件中
                outputStream.Write(buffer, 0, buffer.Length);
            }
            return JsonConvert.SerializeObject(new { Name = fileName });
        }


    }
}