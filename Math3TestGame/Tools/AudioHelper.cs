using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class AudioHelper
    {
        private static AudioHelper instance;

        private Dictionary<SongName, SoundEffect> songs = new Dictionary<SongName, SoundEffect>();

        public static AudioHelper GetInstance()
        {
            if (instance == null) instance = new AudioHelper();
            return instance;
        }

        private AudioHelper()
        {

        }

        public void Play(SongName song)
        {
            if (!GameConfigs.GetInstance().SoundOn) return;

            if (song == SongName.NONE) return;

            songs[song].CreateInstance().Play();
        }
        

        public void SetSong(SongName name, SoundEffect song)
        {
            if (!songs.ContainsKey(name)) songs.Add(name, null);

            songs[name] = song;
        }

        public Song BackgroundMusic { get; set; }

        public void PlayBackground()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.6f;
            MediaPlayer.Play(BackgroundMusic);
        }

        public void StopBackground()
        {
            MediaPlayer.Stop();
        }
    }

    public enum SongName
    {
        NONE = 0,
        BANG = 1,
        LAZER = 2,
        KILL = 3
    }
}
