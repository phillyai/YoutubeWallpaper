﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YoutubeWallpaper
{
    public class Option
    {
        public enum Type
        {
            OneVideo,
            Playlist,
        }

        public enum Quality
        {
            p240,
            p360,
            p480,
            p720,
            p1080,
            p1440,
        }

        //#############################################################################################
        
        public Type IdType
        { get; set; } = Type.OneVideo;

        public string Id
        { get; set; } = "";

        public Quality VideoQuality
        { get; set; } = Quality.p1080;

        public int Volume
        { get; set; } = 100;

        public int ScreenIndex
        { get; set; } = 0;

        public bool IsLive
        { get; set; } = false;

        //#############################################################################################

        public void SaveToFile(string filename)
        {
            using (BinaryWriter bw = new BinaryWriter(new FileStream(filename, FileMode.Create)))
            {
                bw.Write((int)IdType);
                bw.Write(Id);
                bw.Write((int)VideoQuality);
                bw.Write(Volume);
                bw.Write(ScreenIndex);
                bw.Write(IsLive);


                bw.Close();
            }
        }

        public void LoadFromFile(string filename)
        {
            using (BinaryReader br = new BinaryReader(new FileStream(filename, FileMode.Open)))
            {
                try
                {
                    IdType = (Type)br.ReadInt32();
                    Id = br.ReadString();
                    VideoQuality = (Quality)br.ReadInt32();
                    Volume = br.ReadInt32();
                    ScreenIndex = br.ReadInt32();
                    IsLive = br.ReadBoolean();
                }
                catch (EndOfStreamException)
                {
                    // NOTE: 파일이 구버전용임.
                }
                finally
                {
                    br.Close();
                }
            }
        }
    }
}
